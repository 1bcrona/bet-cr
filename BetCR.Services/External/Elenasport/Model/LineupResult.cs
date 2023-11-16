using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BetCR.Services.External.Elenasport.Model
{
    public class LineupResult : BaseElenaResult
    {
        #region Public Properties

        [JsonIgnore]
        public PlayerResult PlayerResult
        {
            get
            {
                var player = (Expand?["player"] as JArray ?? new JArray()).FirstOrDefault();
                return player?.ToObject<PlayerResult>();
            }
        }

        [JsonProperty("position")] public string Position { get; set; }

        [JsonProperty("shirtNumber")] public int ShirtNumber { get; set; }

        [JsonProperty("isStartingXI")] public bool StartingXI { get; set; }

        [JsonProperty("idTeam")] public int TeamId { get; set; }

        #endregion Public Properties
    }
}