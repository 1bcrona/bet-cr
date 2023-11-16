using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Status
    {
        [JsonProperty("long")] public string Long { get; set; }

        [JsonProperty("short")] public string Short { get; set; }

        [JsonProperty("elapsed")] public int? Elapsed { get; set; }
    }
}