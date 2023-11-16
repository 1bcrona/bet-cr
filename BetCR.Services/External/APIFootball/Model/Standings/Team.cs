using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Standings
{
    public class Team
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("logo")] public string Logo { get; set; }
    }
}