using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Goals
    {
        [JsonProperty("home")] public int? Home { get; set; }

        [JsonProperty("away")] public int? Away { get; set; }
    }
}