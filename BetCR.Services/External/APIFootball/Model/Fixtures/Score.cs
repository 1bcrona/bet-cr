using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Score
    {
        [JsonProperty("halftime")] public ScoreDetail Halftime { get; set; }

        [JsonProperty("fulltime")] public ScoreDetail Fulltime { get; set; }

        [JsonProperty("extratime")] public ScoreDetail Extratime { get; set; }

        [JsonProperty("penalty")] public ScoreDetail Penalty { get; set; }
    }
}