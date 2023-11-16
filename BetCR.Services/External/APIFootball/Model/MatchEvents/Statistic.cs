using System.Collections.Generic;
using BetCR.Services.External.APIFootball.Model.Fixtures;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Statistic
    {
        [JsonProperty("team")] public Team Team { get; set; }

        [JsonProperty("statistics")] public List<Statistic> Statistics { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("value")] public object Value { get; set; }

        [JsonProperty("games")] public Games Games { get; set; }

        [JsonProperty("offsides")] public int? Offsides { get; set; }

        [JsonProperty("shots")] public Shots Shots { get; set; }

        [JsonProperty("goals")] public Goals Goals { get; set; }

        [JsonProperty("passes")] public Passes Passes { get; set; }

        [JsonProperty("tackles")] public Tackles Tackles { get; set; }

        [JsonProperty("duels")] public Duels Duels { get; set; }

        [JsonProperty("dribbles")] public Dribbles Dribbles { get; set; }

        [JsonProperty("fouls")] public Fouls Fouls { get; set; }

        [JsonProperty("cards")] public Cards Cards { get; set; }

        [JsonProperty("penalty")] public Penalty Penalty { get; set; }
    }
}