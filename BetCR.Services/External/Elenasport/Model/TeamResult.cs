using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BetCR.Services.External.Elenasport.Model
{
    public class TeamResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("badgeURL")]
        public string BadgeURL { get; set; }

        public List<FixtureResult> FixtureResult
        {
            get
            {
                var next_fixtures = Expand?["next_fixtures"] as JArray;
                return next_fixtures?.ToObject<List<FixtureResult>>();
            }
        }

        [JsonProperty("fullName")]
        public string Name { get; set; }

        #endregion Public Properties
    }
}