using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Passes
    {
        [JsonProperty("total")] public int? Total { get; set; }

        [JsonProperty("key")] public int? Key { get; set; }

        [JsonProperty("accuracy")] public string Accuracy { get; set; }
    }
}