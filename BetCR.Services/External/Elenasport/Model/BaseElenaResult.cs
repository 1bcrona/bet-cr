using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("expand")] public JToken Expand { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        #endregion Public Properties
    }
}