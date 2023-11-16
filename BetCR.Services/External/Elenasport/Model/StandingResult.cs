using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class StandingResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("d")] public int Draw { get; set; }

        [JsonProperty("ga")] public int GoalAgainst { get; set; }

        [JsonProperty("gd")] public int GoalDifference { get; set; }

        [JsonProperty("gf")] public int GoalFor { get; set; }

        [JsonProperty("l")] public int Lost { get; set; }

        [JsonProperty("p")] public int Played { get; set; }

        [JsonProperty("pts")] public int Point { get; set; }

        [JsonProperty("pos")] public int Position { get; set; }

        [JsonIgnore] public string SerializedObject { get; set; }

        [JsonProperty("idTeam")] public string TeamId { get; set; }

        [JsonProperty("teamName")] public string TeamName { get; set; }

        public TeamResult TeamResult
        {
            get
            {
                var team = (Expand?["team"] as JArray ?? new JArray()).FirstOrDefault();
                return team?.ToObject<TeamResult>();
            }
        }

        [JsonProperty("w")] public int Win { get; set; }

        #endregion Public Properties
    }
}