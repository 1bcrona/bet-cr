using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Periods
    {
        [JsonProperty("first")] public int? First { get; set; }

        [JsonProperty("second")] public int? Second { get; set; }
    }
}