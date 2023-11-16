using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Standings
{
    public class Goals
    {
        [JsonProperty("for")]
        public int For { get; set; }

        [JsonProperty("against")]
        public int Against { get; set; }
    }
}