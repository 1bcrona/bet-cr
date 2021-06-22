using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BetCR.Repository.ValueObject;

namespace BetCR.Repository.Entity
{
    public class MatchEvent : EntityBase<string>
    {

        public MatchEvent()
        {
        }
        public MatchEvent(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }
        private Match _match;

        public override string Id { get; set; }

        public MatchEvents Events { get; set; }

        public MatchStats MatchStat { get; set; }
        public MatchLineups MatchLineup { get; set; }
        public int? HomeTeamScore { get; set; }

        public string CurrentElapsed { get; set; }
        public int? AwayTeamScore { get; set; }

        [ForeignKey("MatchId")]
        public Match Match
        {
            get => LazyLoader.Load(this, ref _match);
            set => _match = value;
        }






    }

}