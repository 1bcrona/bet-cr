using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Shots
    {
        [JsonProperty("total")] public int? Total { get; set; }

        [JsonProperty("on")] public int? On { get; set; }
    }
}