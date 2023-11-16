using BetCR.Services.External.APIFootball.Model.Base;
using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Player : BaseApiFootballResult
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("primary")] public string Primary { get; set; }

        [JsonProperty("number")] public int Number { get; set; }

        [JsonProperty("border")] public string Border { get; set; }

        [JsonProperty("pos")] public string Pos { get; set; }

        [JsonProperty("grid")] public string Grid { get; set; }

        [JsonProperty("photo")] public string Photo { get; set; }
    }
}