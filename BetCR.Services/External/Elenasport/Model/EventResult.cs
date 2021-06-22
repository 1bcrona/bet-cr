using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class EventResult : BaseElenaResult
    {
        [JsonProperty("idTeam")]
        public int TeamId { get; set; }


        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty("elapsedPlus")]
        public int ElapsedPlus { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonIgnore]
        public PlayerResult Player1Result
        {
            get
            {
                var player = (Expand?["player1"] as JArray ?? new JArray()).FirstOrDefault();
                return player?.ToObject<PlayerResult>();
            }
        }

        [JsonIgnore]
        public PlayerResult Player2Result
        {
            get
            {
                var player = (Expand?["player2"] as JArray ?? new JArray()).FirstOrDefault();
                return player?.ToObject<PlayerResult>();
            }
        }


    }
}