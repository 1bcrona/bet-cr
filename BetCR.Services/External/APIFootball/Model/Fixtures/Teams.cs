using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Teams
    {
        [JsonProperty("home")] public Team Home { get; set; }

        [JsonProperty("away")] public Team Away { get; set; }
    }
}