using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Base
{
    public class BaseApiFootballResult
    {
        [JsonProperty("id")] public string Id { get; set; }
    }
}