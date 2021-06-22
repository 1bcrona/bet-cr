using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class TeamResult : BaseElenaResult
    {
        [JsonProperty("fullName")]
        public string Name { get; set; }
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
    }
}
