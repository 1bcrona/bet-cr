using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Tackles
    {
        [JsonProperty("total")] public int? Total { get; set; }

        [JsonProperty("blocks")] public int? Blocks { get; set; }

        [JsonProperty("interceptions")] public int? Interceptions { get; set; }
    }
}