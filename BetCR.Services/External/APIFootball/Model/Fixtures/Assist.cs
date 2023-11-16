using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.MatchEvents
{
    public class Assist
    {
        [JsonProperty("id")] public int? Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}