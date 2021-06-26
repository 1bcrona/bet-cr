using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class StageResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("hasStanding")]
        public bool HasStanding { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion Public Properties
    }
}