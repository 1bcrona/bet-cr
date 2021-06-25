using BetCR.Caching.Interface;
using BetCR.Library;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Repository.ValueObject;
using BetCR.Services.External.Elenasport.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetCR.Services.External.Elenasport
{
    public class ElenaFetcherService : IElenaFetcherService
    {
        #region Private Fields

        private const string AUTH_TOKEN_KEY = "Basic NjMxN2luM2djZGRham0wbGxobWF0MHZzaTk6YzFlYXNhajFjdHAxNmw0M2k1OGc1OTJjcGtmMWU3NmRoZW1mdjdhZ2ZkZzZzOW5yZnBl";

        private const string ELENA_FIXTURE_DETAIL_URL = "https://football.elenasport.io/v2/fixtures/#match.ExternalId#?expand=stats,lineups,lineups.player,events,events.player1,events.player2";
        private const string ELENA_FIXTURE_URL = "https://football.elenasport.io/v2/leagues/#league.ExternalId#?expand=current_season,current_season.next_fixtures,current_season.next_fixtures.home_team,current_season.next_fixtures.away_team,current_season.next_fixtures.stage";
        private const string ELENA_STANDINGS_URL = "https://football.elenasport.io/v2/stages/#stage.ExternalId#/standing?expand=team,team.next_fixtures";
        private const string ELENA_TOKEN_URL = "https://oauth2.elenasport.io/oauth2/token";
        private static int _tryCount = 2;
        private readonly ICache _cache;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ElenaFetcherService(IUnitOfWork unitOfWork, ICache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<HttpJsonResult> FetchData(string uri)
        {
            var authorizationToken = await _cache.Get<string>("AUTH_TOKEN") ?? await GetAuthorizationToken();
            var currentCount = 0;
            while (currentCount < _tryCount)
            {
                var request = System.Net.WebRequest.Create(uri);
                request.Method = "GET";
                request.Headers.Add("Authorization", $"Bearer { authorizationToken}");

                var response = (HttpWebResponse)await request.GetResponseAsync();
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
                            return new HttpJsonResult { Data = jsonResponseObject, StatusCode = HttpStatusCode.OK };
                        }
                    default:
                        currentCount++;
                        break;
                }
            }

            return new HttpJsonResult { Data = null, StatusCode = HttpStatusCode.InternalServerError };
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
                var uri = ELENA_FIXTURE_DETAIL_URL.Replace("#match.ExternalId#", match.ExternalId);

                var result = await FetchData(uri);

                if (result.Data["data"] is not JArray data) continue;

                foreach (var t in data)
                {
                    var elenaFixtureResult = t.ToObject<FixtureResult>();

                    var matchEvent = (await matchEventRepository.FindAsync(f => f.Match.ExternalId == match.ExternalId))
                        .FirstOrDefault() ?? new MatchEvent
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            Match = match,
                        };

                    matchEvent.HomeTeamScore = elenaFixtureResult?.HomeTeamRegularGoals + elenaFixtureResult?.HomeTeamExtraTimeGoals;
                    matchEvent.AwayTeamScore = elenaFixtureResult?.AwayTeamRegularGoals + elenaFixtureResult?.AwayTeamExtraTimeGoals;
                    matchEvent.CurrentElapsed = elenaFixtureResult?.Elapsed.ToString();

                    matchEvent.MatchStat = new MatchStats
                    {
                        HomeTeamStats = elenaFixtureResult?.StatResults?.Where(w => w.TeamId.ToString() == match.HomeTeam.ExternalId)
                            .Select(s => new Stat { Key = s.Type, TeamId = match.HomeTeam.Id, Value = s.Value })
                            .ToList(),
                        AwayTeamStats = elenaFixtureResult?.StatResults?.Where(w => w.TeamId.ToString() == match.AwayTeam.ExternalId)
                            .Select(s => new Stat { Key = s.Type, TeamId = match.AwayTeam.Id, Value = s.Value })
                            .ToList(),
                    };

                    matchEvent.MatchLineup = new MatchLineups
                    {
                        HomeTeamLineup = elenaFixtureResult?.LineupResults?.Where(w => w.TeamId.ToString() == match.HomeTeam.ExternalId)
                            .Select(s => new Lineup
                            {
                                TeamId = match.HomeTeam.Id,
                                Number = s.ShirtNumber,
                                StartingXI = s.StartingXI,
                                Position = s.Position.ToUpperInvariant(),
                                Player = new Player { FullName = s.PlayerResult.FullName, Name = s.PlayerResult.Name }
                            }).ToList(),

                        AwayTeamLineup = elenaFixtureResult?.LineupResults?.Where(w => w.TeamId.ToString() == match.AwayTeam.ExternalId)
                            .Select(s => new Lineup
                            {
                                TeamId = match.AwayTeam.Id,
                                Number = s.ShirtNumber,
                                StartingXI = s.StartingXI,
                                Position = s.Position.ToUpperInvariant(),
                                Player = new Player { FullName = s.PlayerResult.FullName, Name = s.PlayerResult.Name }
                            }).ToList(),
                    };

                    matchEvent.Events = new MatchEvents
                    {
                        HomeTeamEvents = elenaFixtureResult?.EventResults?.Where(w => w.TeamId.ToString() == match.HomeTeam.ExternalId)
                            .Select(s => new Event
                            {
                                Elapsed = s.Elapsed,
                                ElapsedPlus = s.ElapsedPlus,
                                TeamId = match.HomeTeam.Id,
                                EventType = s.Type.ToUpperInvariant(),
                                Player1 = new Player
                                {
                                    FullName = s.Player1Result.FullName,
                                    Name = s.Player1Result.Name
                                },
                                Player2 = s.Player2Result != null ? new Player { FullName = s.Player2Result.FullName, Name = s.Player2Result.Name } : null
                            }).ToList(),

                        AwayTeamEvents = elenaFixtureResult?.EventResults?.Where(w => w.TeamId.ToString() == match.AwayTeam.ExternalId)
                            .Select(s => new Event
                            {
                                Elapsed = s.Elapsed,
                                ElapsedPlus = s.ElapsedPlus,
                                TeamId = match.AwayTeam.Id,
                                EventType = s.Type.ToUpperInvariant(),
                                Player1 = new Player
                                {
                                    FullName = s.Player1Result.FullName,
                                    Name = s.Player1Result.Name
                                },
                                Player2 = s.Player2Result != null ? new Player { FullName = s.Player2Result.FullName, Name = s.Player2Result.Name } : null
                            }).ToList()
                    };

                    match.ResultState = elenaFixtureResult != null && elenaFixtureResult.Status == "finished" ? 2 : 1;
                    matchEvent.CurrentElapsed = elenaFixtureResult != null && elenaFixtureResult.Status == "finished" ? "FT" : matchEvent.CurrentElapsed;

                    await matchRepository.UpsertAsync(match);
                    await matchEventRepository.UpsertAsync(matchEvent);

                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task GetFixturesAsync()
        {
            var leagues = _unitOfWork.GetRepository<League, string>();
            var teamRepository = _unitOfWork.GetRepository<Team, string>();
            var stageRepository = _unitOfWork.GetRepository<Stage, string>();
            var matchRepository = _unitOfWork.GetRepository<Match, string>();

            var allLeagues = await leagues.GetAllAsync();

            foreach (var league in allLeagues)
            {
                var uri = ELENA_FIXTURE_URL.Replace("#league.ExternalId#", league.ExternalId);

                var result = await FetchData(uri);

                if (result.Data["data"] is not JArray data) continue;

                foreach (var t in data)
                {
                    var expand = t["expand"];
                    var season = (expand?["current_season"] as JArray ?? new JArray()).FirstOrDefault();
                    if (season == null) continue;
                    var seasonExpand = season["expand"];
                    var nextFixtures = seasonExpand["next_fixtures"];
                    if (nextFixtures is not JArray nextFixtureArray) continue;
                    foreach (var nf in nextFixtureArray)
                    {
                        var elenaFixture = nf.ToObject<FixtureResult>();

                        if (elenaFixture.Status != "not started") continue;
                        if (String.IsNullOrEmpty(elenaFixture.Id)) continue;
                        var existingMatch = (await matchRepository.FindAsync(f => f.ExternalId == elenaFixture.Id)).FirstOrDefault();
                        if (existingMatch != null) continue;

                        var fixtureExpand = nf["expand"];
                        var homeTeamJsonObject = (fixtureExpand["home_team"] as JArray ?? new JArray()).FirstOrDefault();
                        var awayTeamJsonObject = (fixtureExpand["away_team"] as JArray ?? new JArray()).FirstOrDefault();
                        var stageJsonObject = (fixtureExpand["stage"] as JArray ?? new JArray()).FirstOrDefault();

                        var elenaHomeTeam = homeTeamJsonObject.ToObject<TeamResult>();
                        var elenaAwayTeam = awayTeamJsonObject.ToObject<TeamResult>();
                        var elenaStage = stageJsonObject.ToObject<StageResult>();

                        if (String.IsNullOrEmpty(elenaHomeTeam.Id)) continue;
                        if (String.IsNullOrEmpty(elenaAwayTeam.Id)) continue;
                        if (String.IsNullOrEmpty(elenaStage.Id)) continue;

                        var homeTeamObject = (await teamRepository.FindAsync(f => f.ExternalId == elenaHomeTeam.Id)).FirstOrDefault() ??
                                             new Team
                                             {
                                                 Active = 1,
                                                 ExternalId = elenaHomeTeam.Id,
                                                 Id = Guid.NewGuid().ToString("D"),
                                                 TeamLogo = elenaHomeTeam.BadgeURL,
                                                 Name = elenaHomeTeam.Name,
                                                 UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                                             };

                        if (homeTeamObject.Name == null) continue;

                        var homeDominantColors = TeamHelper.GetColorsOfTeam(elenaHomeTeam);
                        homeTeamObject.DominantColors = homeDominantColors;

                        var awayTeamObject = (await teamRepository.FindAsync(f => f.ExternalId == elenaAwayTeam.Id)).FirstOrDefault() ??
                                             new Team
                                             {
                                                 Active = 1,
                                                 ExternalId = elenaAwayTeam.Id,
                                                 Id = Guid.NewGuid().ToString("D"),
                                                 TeamLogo = elenaAwayTeam.BadgeURL,
                                                 Name = elenaAwayTeam.Name,
                                                 UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                                             };
                        if (awayTeamObject.Name == null) continue;

                        var awayDominantColors = TeamHelper.GetColorsOfTeam(elenaAwayTeam);
                        awayTeamObject.DominantColors = awayDominantColors;
                        await teamRepository.UpsertAsync(homeTeamObject);
                        await teamRepository.UpsertAsync(awayTeamObject);
                        elenaFixture.MatchDate = DateTime.SpecifyKind(elenaFixture.MatchDate, DateTimeKind.Utc);

                        var stageObject = (await stageRepository.FindAsync(f => f.ExternalId == elenaStage.Id)).FirstOrDefault();
                        if (stageObject == null)
                        {
                            stageObject = new Stage
                            {
                                Id = Guid.NewGuid().ToString("D"),
                                ExternalId = elenaStage.Id,
                                StageName = elenaStage.Name,
                                LeagueId = league.Id,
                                UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                            };
                            await stageRepository.AddAsync(stageObject);
                        }

                        var match = new Match
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            ExternalId = elenaFixture.Id,
                            HomeTeam = homeTeamObject,
                            AwayTeam = awayTeamObject,
                            MatchDateEpoch = ((DateTimeOffset)elenaFixture.MatchDate).ToUnixTimeSeconds(),
                            Week = FixtureHelper.GetWeek(elenaFixture.MatchDate),
                            Stage = stageObject,
                            ResultState = 1,
                            UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                        };

                        await matchRepository.AddAsync(match);
                        await _unitOfWork.SaveChangesAsync();
                    }
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
                var uri = ELENA_STANDINGS_URL.Replace("#stage.ExternalId#", stage.ExternalId);

                var result = await FetchData(uri);

                if (result.Data["data"] is not JArray data) continue;

                var stageStanding = (await stageStandingRepository.FindAsync(f => f.Stage.Id == stage.Id))
                    .FirstOrDefault() ?? new StageStanding
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Stage = stage,
                    };

                stageStanding.Standings = new List<Standing>();
                foreach (var t in data)
                {
                    var elenaStandingResult = t.ToObject<StandingResult>();

                    if (elenaStandingResult == null) continue;

                    var standing = new Standing
                    {
                        Draw = elenaStandingResult.Draw,
                        GoalAgainst = elenaStandingResult.GoalAgainst,
                        GoalDifference = elenaStandingResult.GoalDifference,
                        GoalFor = elenaStandingResult.GoalFor,
                        Lost = elenaStandingResult.Lost,
                        Played = elenaStandingResult.Played,
                        Point = elenaStandingResult.Point,
                        Win = elenaStandingResult.Win,
                        TeamStanding = new TeamStanding
                        {
                            Position = elenaStandingResult.Position,
                            BadgeURL = elenaStandingResult.TeamResult.BadgeURL,
                            TeamName = elenaStandingResult.TeamName,
                            ExternalId = elenaStandingResult.TeamId
                        }
                    };

                    stageStanding.Standings.Add(standing);
                }

                stageStanding.Serialized = JsonConvert.SerializeObject(data);

                stage.StageStanding = stageStanding;
                await stageStandingRepository.UpsertAsync(stageStanding);
                await stageRepository.UpsertAsync(stage);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<string> GetAuthorizationToken()
        {
            var webRequest = System.Net.WebRequest.Create(ELENA_TOKEN_URL);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Headers.Add("Authorization", AUTH_TOKEN_KEY);
            var data = Encoding.ASCII.GetBytes(@$"grant_type={Uri.EscapeDataString("client_credentials")}");
            webRequest.ContentLength = data.Length;

            await using (var stream = await webRequest.GetRequestStreamAsync())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }

            var response = (HttpWebResponse)await webRequest.GetResponseAsync();
            if (response.StatusCode != HttpStatusCode.OK) return String.Empty;

            await using (var s = response.GetResponseStream())
            {
                using var sr = new System.IO.StreamReader(s);
                var jsonResponse = await sr.ReadToEndAsync();

                var tokenResult = JsonConvert.DeserializeObject<TokenResult>(jsonResponse);

                await _cache.Set("AUTH_TOKEN", tokenResult.Token, TimeSpan.FromSeconds(tokenResult.ExpiresIn));

                return tokenResult.Token;
            }
        }

        #endregion Private Methods
    }
}