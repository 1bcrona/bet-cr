using Newtonsoft.Json;

namespace BetCR.Services.External.APIFootball.Model.Fixtures
{
    public class Venue
    {
        [JsonProperty("id")] public int? Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("city")] public string City { get; set; }
    }
}