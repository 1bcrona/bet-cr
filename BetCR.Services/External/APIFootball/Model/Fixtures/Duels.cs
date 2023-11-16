using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Duels
    {
        [JsonProperty("total")] public int? Total { get; set; }

        [JsonProperty("won")] public int? Won { get; set; }
    }
}