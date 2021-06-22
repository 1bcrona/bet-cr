using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BetCR.Repository.Entity
{
    public class League : EntityBase<string>
    {

        public override string Id { get; set; }

        public string ExternalId { get; set; }

        public string LeagueName { get; set; }

        [IgnoreDataMember]
        public ICollection<Stage> Stages { get; set; }



        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Match> Matches { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<TeamLeagueRel> TeamLeagueRels { get; set; }
    }

}