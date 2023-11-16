using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Substitute
    {
        [JsonProperty("player")] public Player Player { get; set; }
    }
}