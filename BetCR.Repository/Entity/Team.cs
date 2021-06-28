using BetCR.Repository.Entity.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class Team : BaseEntity<string>
    {
        #region Public Properties

        [IgnoreDataMember]
        public IEnumerable<Match> AwayMatches { get; set; }

        public string DominantColors { get; set; }
        public string ExternalId { get; set; }

        [IgnoreDataMember]
        public IEnumerable<Match> HomeMatches { get; set; }

        public override string Id { get; set; }
        public string Name { get; set; }

        [IgnoreDataMember]
        public IEnumerable<TeamLeagueRel> TeamLeagueRels { get; set; }

        public string TeamLogo { get; set; }

        #endregion Public Properties
    }
}