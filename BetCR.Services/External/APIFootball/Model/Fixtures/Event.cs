using BetCR.Services.External.APIFootball.Model.Fixtures;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Event
    {
        [JsonProperty("time")] public Time Time { get; set; }

        [JsonProperty("team")] public Team Team { get; set; }

        [JsonProperty("player")] public Player Player { get; set; }

        [JsonProperty("assist")] public Assist Assist { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("detail")] public string Detail { get; set; }
    }
}