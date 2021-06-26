using BetCR.Repository.Entity.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BetCR.Repository.Entity
{
    public class League : EntityBase<string>
    {
        #region Public Properties

        public string ExternalId { get; set; }
        public override string Id { get; set; }
        public string LeagueName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Match> Matches { get; set; }

        [IgnoreDataMember]
        public ICollection<Stage> Stages { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<TeamLeagueRel> TeamLeagueRels { get; set; }

        #endregion Public Properties
    }
}