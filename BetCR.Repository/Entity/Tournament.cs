using System;
using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class Tournament : EntityBase<string>
    {

        public override string Id { get; set; }

        public string TournamentName { get; set; }

        public DateTime TournamentStartDate { get; set; }
        public DateTime TournamentEndDate { get; set; }

        [IgnoreDataMember]
        public ICollection<Match> Matches { get; set; }


    }

}