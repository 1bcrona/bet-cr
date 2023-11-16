using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Cards
    {
        [JsonProperty("yellow")] public int Yellow { get; set; }

        [JsonProperty("red")] public int Red { get; set; }
    }
}