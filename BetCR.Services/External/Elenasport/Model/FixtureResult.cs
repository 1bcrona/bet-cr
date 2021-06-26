using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace BetCR.Services.External.Elenasport.Model
{
    public class FixtureResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("team_away_ET_goals")]
        public int AwayTeamExtraTimeGoals { get; set; }

        [JsonProperty("team_away_1stHalf_goals")]
        public int AwayTeamFirstHalfGoals { get; set; }

        [JsonProperty("team_away_PEN_goals")]
        public int AwayTeamPenaltyGoals { get; set; }

        [JsonProperty("team_away_90min_goals")]
        public int AwayTeamRegularGoals { get; set; }

        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        public List<EventResult> EventResults
        {
            get
            {
                var token = Expand?["events"] as JArray ?? new JArray();
                return token.ToObject<List<EventResult>>();
            }
        }

        [JsonProperty("team_home_ET_goals")]
        public int HomeTeamExtraTimeGoals { get; set; }

        [JsonProperty("team_home_1stHalf_goals")]
        public int HomeTeamFirstHalfGoals { get; set; }

        [JsonProperty("team_home_PEN_goals")]
        public int HomeTeamPenaltyGoals { get; set; }

        [JsonProperty("team_home_90min_goals")]
        public int HomeTeamRegularGoals { get; set; }

        public List<LineupResult> LineupResults
        {
            get
            {
                var token = Expand?["lineups"] as JArray ?? new JArray();
                return token.ToObject<List<LineupResult>>();
            }
        }

        [JsonProperty("date")]
        public DateTime MatchDate { get; set; }

        public List<StatResult> StatResults
        {
            get
            {
                var token = Expand?["stats"] as JArray ?? new JArray();
                return token.ToObject<List<StatResult>>();
            }
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        #endregion Public Properties
    }
}