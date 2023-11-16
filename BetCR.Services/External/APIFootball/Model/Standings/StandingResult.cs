using System;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Standings
{
    public class StandingResult
    {
        [JsonProperty("rank")] public int Rank { get; set; }

        [JsonProperty("team")] public Team Team { get; set; }

        [JsonProperty("points")] public int Points { get; set; }

        [JsonProperty("goalsDiff")] public int GoalsDiff { get; set; }

        [JsonProperty("group")] public string Group { get; set; }

        [JsonProperty("form")] public string Form { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("all")] public StandingStats All { get; set; }

        [JsonProperty("home")] public StandingStats Home { get; set; }

        [JsonProperty("away")] public StandingStats Away { get; set; }

        [JsonProperty("update")] public DateTime Update { get; set; }
    }
}