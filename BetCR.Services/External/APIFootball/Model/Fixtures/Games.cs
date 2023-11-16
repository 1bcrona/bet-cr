using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Games
    {
        [JsonProperty("minutes")] public int? Minutes { get; set; }

        [JsonProperty("number")] public int Number { get; set; }

        [JsonProperty("position")] public string Position { get; set; }

        [JsonProperty("rating")] public string Rating { get; set; }

        [JsonProperty("captain")] public bool Captain { get; set; }

        [JsonProperty("substitute")] public bool Substitute { get; set; }
    }
}