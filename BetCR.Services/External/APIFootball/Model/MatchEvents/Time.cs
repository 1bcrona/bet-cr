using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Time
    {
        [JsonProperty("elapsed")] public int Elapsed { get; set; }

        [JsonProperty("extra")] public int? Extra { get; set; }
    }
}