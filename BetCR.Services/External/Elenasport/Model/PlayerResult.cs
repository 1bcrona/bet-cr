using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class PlayerResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("fullName")] public string FullName { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        #endregion Public Properties
    }
}