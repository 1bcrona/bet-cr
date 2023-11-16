using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Goals
    {
        [JsonProperty("total")] public int? Total { get; set; }

        [JsonProperty("conceded")] public int Conceded { get; set; }

        [JsonProperty("assists")] public int? Assists { get; set; }

        [JsonProperty("saves")] public int? Saves { get; set; }
    }
}