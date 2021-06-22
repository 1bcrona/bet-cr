using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class StageResult : BaseElenaResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hasStanding")]
        public bool HasStanding { get; set; }

    }
}
