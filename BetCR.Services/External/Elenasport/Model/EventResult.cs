using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class EventResult : BaseElenaResult
    {
        #region Public Properties

        [JsonProperty("elapsed")] public int Elapsed { get; set; }

        [JsonProperty("elapsedPlus")] public int ElapsedPlus { get; set; }

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

        [JsonProperty("idTeam")] public int TeamId { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        #endregion Public Properties
    }
}