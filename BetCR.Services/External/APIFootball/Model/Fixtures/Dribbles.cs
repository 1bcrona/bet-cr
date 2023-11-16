using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Dribbles
    {
        [JsonProperty("attempts")] public int? Attempts { get; set; }

        [JsonProperty("success")] public int? Success { get; set; }

        [JsonProperty("past")] public int? Past { get; set; }
    }
}