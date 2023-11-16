using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BetCR.Caching.Interface;
using BetCR.Library;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Repository.ValueObject;
using BetCR.Services.External.APIFootball.Model.Fixtures;
using BetCR.Services.External.Elenasport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StandingResult = BetCR.Services.External.APIFootball.Model.Standings.StandingResult;
using Team = BetCR.Repository.Entity.Team;

namespace BetCR.Services.External.APIFootball
{
    public class ApiFootballFetcher : IElenaFetcherService
    {
        #region Private Fields

        private static readonly List<Tuple<Func<Model.MatchEvents.Event, bool>, string>> _eventDictionary = new();


        private static JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        });

        private const string AUTH_TOKEN_KEY = "ad4806328ed84e418075fb7e970fae79";
        private const string API_FOOTBALL_FIXTURE_DETAIL_URL = "https://v3.football.api-sports.io/fixtures?id=#match.ExternalId#";
        private const string API_FOOTBALL_FIXTURES_URL = "https://v3.football.api-sports.io/fixtures?league=#league_external_id#&season=#season_external_id#&from=#from_date#&to=#to_date#";
        private const string API_FOOTBALL_STANDINGS_URL = "https://v3.football.api-sports.io/standings?league=#league_external_id#&season=#season_external_id#";
        private static int _tryCount = 2;
        private readonly ICache _cache;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ApiFootballFetcher(IUnitOfWork unitOfWork, ICache cache)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }


        static ApiFootballFetcher()
        {
            _eventDictionary = new List<Tuple<Func<Model.MatchEvents.Event, bool>, string>>
            {
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Goal" && @event.Detail == "Normal Goal", "GOAL"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Goal" && @event.Detail == "Penalty", "PEN_SCORED"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Goal" && @event.Detail == "Own Goal", "OWN_GOAL"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Goal" && @event.Detail == "Missed Penalty", "PEN_MISSED"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Card" && @event.Detail == "Yellow Card", "Y_CARD"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "Card" && @event.Detail == "Red Card", "R_CARD"),
                new Tuple<Func<Model.MatchEvents.Event, bool>, string>(@event => @event.Type == "subst", "SUBST")
            };
        }

        #endregion Public Constructors

        #region Public Methods

        private async Task<HttpJsonResult> FetchData(string uri)
        {
            var authorizationToken = await _cache.Get<string>("AUTH_TOKEN") ?? await GetAuthorizationToken();
            var currentCount = 0;
            while (currentCount < _tryCount)
            {
                var request = WebRequest.Create(uri);
                request.Method = "GET";
                request.Headers.Add("x-apisports-key", $"{authorizationToken}");

                var response = (HttpWebResponse) await request.GetResponseAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        await _cache.Delete("AUTH_TOKEN");
                        return await FetchData(uri);

                    case HttpStatusCode.OK:
                    {
                        await using var s = response.GetResponseStream();
                        using var sr = new System.IO.StreamReader(s);
                        var jsonResponse = await sr.ReadToEndAsync();
                        var jsonResponseObject = JObject.Parse(jsonResponse);
                        return new HttpJsonResult {Data = jsonResponseObject, StatusCode = HttpStatusCode.OK};
                    }

                    default:
                        currentCount++;
                        break;
                }
            }

            return new HttpJsonResult {Data = null, StatusCode = HttpStatusCode.InternalServerError};
        }

        public async Task GetFixtureResultsAsync()
        {
            var matchRepository = _unitOfWork.GetRepository<Match, string>();
            var matchEventRepository = _unitOfWork.GetRepository<MatchEvent, string>();

            var currentEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var matches = (await matchRepository.FindAsync(f =>
                f.Active == 1 && f.MatchDateEpoch < currentEpoch && f.ResultState == 1)).ToList();

            foreach (var match in matches)
            {
                var uri = API_FOOTBALL_FIXTURE_DETAIL_URL.Replace("#match.ExternalId#", match.ExternalId);

                var result = await FetchData(uri);

                if (result.Data["response"] is not JArray data) continue;

                foreach (var t in data)
                {
                    await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
                    var fixtureResult = t.ToObject<FixturesResult>(_serializer);

                    var matchEvent = (await matchEventRepository.FindAsync(f => f.Match.ExternalId == match.ExternalId))
                        .FirstOrDefault() ?? new MatchEvent
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            Match = match
                        };

                    matchEvent.HomeTeamScore = fixtureResult?.Goals.Home;
                    matchEvent.AwayTeamScore = fixtureResult?.Goals.Away;
                    matchEvent.CurrentElapsed = fixtureResult?.Fixture.Status.Elapsed.ToString();
                    matchEvent.MatchLineup = new MatchLineups
                    {
                        HomeTeamLineup = fixtureResult?.Lineups?.Where(w => w.Team.Id.ToString() == match.HomeTeam.ExternalId).SelectMany(s => s.StartXI)
                            .Select(s => new Lineup
                            {
                                TeamId = match.HomeTeam.Id,
                                Number = s.Player.Number,
                                StartingXI = true,
                                Position = s.Player.Pos switch
                                {
                                    "G" => "GOALKEEPER",
                                    "D" => "DEFENDER",
                                    "M" => "MIDFIELDER",
                                    "F" => "FORWARD",
                                    _ => "NONE"
                                },
                                Player = new Player {FullName = s.Player.Name, Name = s.Player.Name}
                            }).ToList(),

                        AwayTeamLineup = fixtureResult?.Lineups?.Where(w => w.Team.Id.ToString() == match.AwayTeam.ExternalId).SelectMany(s => s.StartXI)
                            .Select(s => new Lineup
                            {
                                TeamId = match.HomeTeam.Id,
                                Number = s.Player.Number,
                                StartingXI = true,
                                Position = s.Player.Pos switch
                                {
                                    "G" => "GOALKEEPER",
                                    "D" => "DEFENDER",
                                    "M" => "MIDFIELDER",
                                    "F" => "FORWARD",
                                    _ => "NONE"
                                },
                                Player = new Player {FullName = s.Player.Name, Name = s.Player.Name}
                            }).ToList()
                    };


                    var homeSubs = fixtureResult?.Lineups?.Where(w => w.Team.Id.ToString() == match.HomeTeam.ExternalId).SelectMany(s => s.Substitutes)
                        .Select(s => new Lineup
                        {
                            TeamId = match.HomeTeam.Id,
                            Number = s.Player.Number,
                            StartingXI = false,
                            Position = s.Player.Pos switch
                            {
                                "G" => "GOALKEEPER",
                                "D" => "DEFENDER",
                                "M" => "MIDFIELDER",
                                "F" => "FORWARD",
                                _ => "NONE"
                            },
                            Player = new Player {FullName = s.Player.Name, Name = s.Player.Name}
                        }).ToList();

                    if (homeSubs != null) matchEvent.MatchLineup.HomeTeamLineup?.AddRange(homeSubs);

                    var awaySubs = fixtureResult?.Lineups?.Where(w => w.Team.Id.ToString() == match.AwayTeam.ExternalId).SelectMany(s => s.Substitutes)
                        .Select(s => new Lineup
                        {
                            TeamId = match.HomeTeam.Id,
                            Number = s.Player.Number,
                            StartingXI = false,
                            Position = s.Player.Pos switch
                            {
                                "G" => "GOALKEEPER",
                                "D" => "DEFENDER",
                                "M" => "MIDFIELDER",
                                "F" => "FORWARD",
                                _ => "NONE"
                            },
                            Player = new Player {FullName = s.Player.Name, Name = s.Player.Name}
                        }).ToList();

                    if (awaySubs != null) matchEvent.MatchLineup.AwayTeamLineup?.AddRange(awaySubs);

                    matchEvent.MatchLineup.AwayTeamLineup?.AddRange(awaySubs);


                    matchEvent.Events = new MatchEvents
                    {
                        HomeTeamEvents = fixtureResult?.Events?.Where(w => w.Team.Id.ToString() == match.HomeTeam.ExternalId)
                            .Select(s => new Event
                            {
                                Elapsed = Math.Abs(s.Time.Elapsed),
                                ElapsedPlus = s.Time.Extra ?? 0,
                                TeamId = match.HomeTeam.Id,
                                EventType = (from e in _eventDictionary
                                    where Convert.ToBoolean(e.Item1.DynamicInvoke(s))
                                    select e.Item2).FirstOrDefault(),
                                Player1 = new Player
                                {
                                    FullName = s.Player.Name,
                                    Name = s.Player.Name
                                },
                                Player2 = s.Assist != null ? new Player {FullName = s.Assist?.Name, Name = s.Assist?.Name} : null
                            }).ToList(),

                        AwayTeamEvents = fixtureResult?.Events?.Where(w => w.Team.Id.ToString() == match.AwayTeam.ExternalId)
                            .Select(s => new Event
                            {
                                Elapsed = Math.Abs(s.Time.Elapsed),
                                ElapsedPlus = s.Time.Extra ?? 0,
                                TeamId = match.HomeTeam.Id,
                                EventType = (from e in _eventDictionary
                                    where Convert.ToBoolean(e.Item1.DynamicInvoke(s))
                                    select e.Item2).FirstOrDefault(),
                                Player1 = new Player
                                {
                                    FullName = s.Player.Name,
                                    Name = s.Player.Name
                                },
                                Player2 = s.Assist != null ? new Player {FullName = s.Assist?.Name, Name = s.Assist?.Name} : null
                            }).ToList()
                    };

                    match.ResultState = fixtureResult?.Fixture.Status.Short == "FT" ? 2 : 1;
                    matchEvent.CurrentElapsed = fixtureResult?.Fixture.Status.Short == "FT" ? "90" : fixtureResult?.Fixture.Status.Elapsed.ToString();

                    await matchRepository.UpsertAsync(match);
                    await _unitOfWork.SaveChangesAsync();

                    await matchEventRepository.UpsertAsync(matchEvent);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
        }

        public async Task GetFixturesAsync()
        {
            var teamRepository = _unitOfWork.GetRepository<Team, string>();
            var stageRepository = _unitOfWork.GetRepository<Stage, string>();
            var matchRepository = _unitOfWork.GetRepository<Match, string>();

            var stages = await stageRepository.GetAllAsync();

            foreach (var stage in stages)
            {
                var uri = API_FOOTBALL_FIXTURES_URL.Replace("#league_external_id#", stage.League.ExternalId)
                    .Replace("#season_external_id#", stage.ExternalId)
                    .Replace("#from_date#", DateTimeOffset.UtcNow.Date.AddDays(-15).ToString("yyyy-MM-dd"))
                    .Replace("#to_date#", DateTimeOffset.UtcNow.Date.AddDays(15).ToString("yyyy-MM-dd"));

                var result = await FetchData(uri);

                if (result.Data["response"] is not JArray data) continue;

                foreach (var t in data)
                {
                    await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
                    var fixtureResult = t.ToObject<FixturesResult>(_serializer);
                    if (string.IsNullOrEmpty(fixtureResult.Fixture.Id)) continue;
                    var existingMatch = (await matchRepository.FindAsync(f => f.ExternalId == fixtureResult.Fixture.Id)).FirstOrDefault();
                    if (existingMatch != null) continue;


                    var homeTeamObject = (await teamRepository.FindAsync(f => f.ExternalId == fixtureResult.Teams.Home.Id)).FirstOrDefault() ??
                                         new Team
                                         {
                                             Active = 1,
                                             ExternalId = fixtureResult.Teams.Home.Id,
                                             Id = Guid.NewGuid().ToString("D"),
                                             TeamLogo = fixtureResult.Teams.Home.Logo,
                                             Name = fixtureResult.Teams.Home.Name,
                                             UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                                         };

                    if (homeTeamObject.Name == null) continue;

                    var homeDominantColors = TeamHelper.GetColorsOfLogo(homeTeamObject.TeamLogo);
                    homeTeamObject.DominantColors = homeDominantColors;

                    var awayTeamObject = (await teamRepository.FindAsync(f => f.ExternalId == fixtureResult.Teams.Away.Id)).FirstOrDefault() ??
                                         new Team
                                         {
                                             Active = 1,
                                             ExternalId = fixtureResult.Teams.Away.Id,
                                             Id = Guid.NewGuid().ToString("D"),
                                             TeamLogo = fixtureResult.Teams.Away.Logo,
                                             Name = fixtureResult.Teams.Away.Name,
                                             UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                                         };

                    if (awayTeamObject.Name == null) continue;

                    var awayDominantColors = TeamHelper.GetColorsOfLogo(awayTeamObject.TeamLogo);
                    awayTeamObject.DominantColors = awayDominantColors;
                    await teamRepository.UpsertAsync(homeTeamObject);
                    await _unitOfWork.SaveChangesAsync();
                    await teamRepository.UpsertAsync(awayTeamObject);
                    await _unitOfWork.SaveChangesAsync();

                    var stageObject = (await stageRepository.FindAsync(f => f.ExternalId == fixtureResult.League.Season.ToString())).FirstOrDefault();
                    if (stageObject == null)
                    {
                        stageObject = new Stage
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            ExternalId = fixtureResult.League.Season.ToString(),
                            StageName = fixtureResult.League.Season.ToString(),
                            LeagueId = stage.League.Id,
                            UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                        };
                        await stageRepository.AddAsync(stageObject);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    var match = new Match
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        ExternalId = fixtureResult.Fixture.Id,
                        HomeTeam = homeTeamObject,
                        AwayTeam = awayTeamObject,
                        MatchDateEpoch = fixtureResult.Fixture.Timestamp,
                        Week = FixtureHelper.GetWeek(fixtureResult.Fixture.Date),
                        Stage = stageObject,
                        ResultState = 1,
                        UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    };

                    await matchRepository.AddAsync(match);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
        }

        public async Task GetStandingsAsync()
        {
            var stageRepository = _unitOfWork.GetRepository<Stage, string>();
            var stageStandingRepository = _unitOfWork.GetRepository<StageStanding, string>();

            var stages = (await stageRepository.FindAsync(f => f.Active == 1 && f.HasStanding)).ToList();

            foreach (var stage in stages)
            {
                await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
                var uri = API_FOOTBALL_STANDINGS_URL.Replace("#league_external_id#", stage.League.ExternalId)
                    .Replace("#season_external_id#", stage.ExternalId);
                var result = await FetchData(uri);

                if (result.Data?["response"] is not JArray data) continue;
                var stageStanding = (await stageStandingRepository.FindAsync(f => f.Stage.Id == stage.Id))
                    .FirstOrDefault() ?? new StageStanding
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Stage = stage
                    };
                stageStanding.Standings = new List<Standing>();
                var standings = data[0]["league"]?["standings"]?.FirstOrDefault();
                if (standings == null) continue;

                foreach (var t in standings)
                {
                    var elenaStandingResult = t.ToObject<StandingResult>(_serializer);

                    if (elenaStandingResult == null) continue;

                    var standing = new Standing
                    {
                        Draw = elenaStandingResult.All.Draw,
                        GoalAgainst = elenaStandingResult.All.Goals.Against,
                        GoalFor = elenaStandingResult.All.Goals.For,
                        GoalDifference = elenaStandingResult.GoalsDiff,
                        Lost = elenaStandingResult.All.Lose,
                        Played = elenaStandingResult.All.Played,
                        Point = elenaStandingResult.Points,
                        Win = elenaStandingResult.All.Win,
                        TeamStanding = new TeamStanding
                        {
                            Position = elenaStandingResult.Rank,
                            BadgeURL = elenaStandingResult.Team.Logo,
                            TeamName = elenaStandingResult.Team.Name,
                            ExternalId = elenaStandingResult.Team.Id.ToString()
                        }
                    };

                    stageStanding.Standings.Add(standing);
                }

                stageStanding.Serialized = JsonConvert.SerializeObject(data);

                stage.StageStanding = stageStanding;
                await stageStandingRepository.UpsertAsync(stageStanding);
                await _unitOfWork.SaveChangesAsync();
                await stageRepository.UpsertAsync(stage);
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<string> GetAuthorizationToken()
        {
            return await Task.FromResult(AUTH_TOKEN_KEY);
        }

        #endregion Private Methods
    }
}