using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class StartXI
    {
        [JsonProperty("player")] public Player Player { get; set; }
    }
}