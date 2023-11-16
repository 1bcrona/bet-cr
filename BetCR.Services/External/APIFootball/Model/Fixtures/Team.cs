using BetCR.Services.External.APIFootball.Model.Base;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Team : BaseApiFootballResult
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("logo")] public string Logo { get; set; }

        [JsonProperty("winner")] public bool Winner { get; set; }
    }
}