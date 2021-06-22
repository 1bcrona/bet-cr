using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class TokenResult
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
