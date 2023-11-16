using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class TokenResult
    {
        #region Public Properties

        [JsonProperty("expires_in")] public int ExpiresIn { get; set; }

        [JsonProperty("access_token")] public string Token { get; set; }

        #endregion Public Properties
    }
}