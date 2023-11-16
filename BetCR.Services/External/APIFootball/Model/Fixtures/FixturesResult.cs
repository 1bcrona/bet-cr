using System.Collections.Generic;
using BetCR.Services.External.APIFootball.Model.MatchEvents;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class FixturesResult
    {
        [JsonProperty("fixture")] public Fixture Fixture { get; set; }

        [JsonProperty("league")] public League League { get; set; }

        [JsonProperty("teams")] public Teams Teams { get; set; }

        [JsonProperty("goals")] public Goals Goals { get; set; }

        [JsonProperty("score")] public Score Score { get; set; }

        [JsonProperty("events")] public List<Event> Events { get; set; }

        [JsonProperty("lineups")] public List<Lineup> Lineups { get; set; }

        [JsonProperty("statistics")] public List<Statistic> Statistics { get; set; }

        [JsonProperty("players")] public List<Player> Players { get; set; }
    }
}