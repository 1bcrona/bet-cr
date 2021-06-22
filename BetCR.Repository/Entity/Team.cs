using System;
using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class Team : EntityBase<string>
    {

        public override string Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string TeamLogo { get; set; }

        [IgnoreDataMember]
        public IEnumerable<Match> HomeMatches { get; set; }
        [IgnoreDataMember]
        public IEnumerable<Match> AwayMatches { get; set; }
        [IgnoreDataMember]
        public IEnumerable<TeamLeagueRel> TeamLeagueRels { get; set; }

        public string DominantColors { get; set; }
    }

}