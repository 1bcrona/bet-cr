using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Fouls
    {
        [JsonProperty("drawn")] public int? Drawn { get; set; }

        [JsonProperty("committed")] public int? Committed { get; set; }
    }
}