using Newtonsoft.Json;

namespace BetCR.Services.External.Elenasport.Model
{
    public class PlayerResult : BaseElenaResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

    }
}