using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class BaseElenaResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expand")]

        public JToken Expand { get; set; }

    }
}
