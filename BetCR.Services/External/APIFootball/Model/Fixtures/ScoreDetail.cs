using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class ScoreDetail
    {
        [JsonProperty("home")] public int? Home { get; set; }

        [JsonProperty("away")] public int? Away { get; set; }
    }
}