using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class StatResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("idTeam")] public int TeamId { get; set; }

        [JsonProperty("label")] public string Type { get; set; }

        [JsonProperty("value")] public int Value { get; set; }

        #endregion Public Properties
    }
}