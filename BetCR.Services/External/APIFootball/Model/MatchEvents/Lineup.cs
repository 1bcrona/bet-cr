using System.Collections.Generic;
using BetCR.Services.External.APIFootball.Model.Fixtures;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Lineup
    {
        [JsonProperty("team")] public Team Team { get; set; }

        [JsonProperty("coach")] public Coach Coach { get; set; }

        [JsonProperty("formation")] public string Formation { get; set; }

        [JsonProperty("startXI")] public List<StartXI> StartXI { get; set; }

        [JsonProperty("substitutes")] public List<Substitute> Substitutes { get; set; }
    }
}