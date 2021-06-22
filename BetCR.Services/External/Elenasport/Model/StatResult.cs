using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class StatResult : BaseElenaResult
    {
        [JsonProperty("label")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("idTeam")]
        public int TeamId { get; set; }

    }
}