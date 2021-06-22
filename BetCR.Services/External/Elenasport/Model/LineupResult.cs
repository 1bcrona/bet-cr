using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class LineupResult : BaseElenaResult
    {
        [JsonProperty("idTeam")]
        public int TeamId { get; set; }
        [JsonProperty("isStartingXI")]
        public bool StartingXI { get; set; }

        [JsonProperty("shirtNumber")]
        public int ShirtNumber { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonIgnore]
        public PlayerResult PlayerResult
        {
            get
            {
                var player = (Expand?["player"] as JArray ?? new JArray()).FirstOrDefault();
                return player?.ToObject<PlayerResult>();
            }
        }
    }
}