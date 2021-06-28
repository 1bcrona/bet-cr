using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetCR.Repository.Entity
{
    public class MatchEvent : BaseEntity<string>
    {
        #region Private Fields

        private Match _match;

        #endregion Private Fields

        #region Public Constructors

        public MatchEvent()
        {
        }

        public MatchEvent(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public int? AwayTeamScore { get; set; }
        public string CurrentElapsed { get; set; }
        public MatchEvents Events { get; set; }
        public int? HomeTeamScore { get; set; }
        public override string Id { get; set; }

        [ForeignKey("MatchId")]
        public Match Match
        {
            get => LazyLoader.Load(this, ref _match);
            set => _match = value;
        }

        public MatchLineups MatchLineup { get; set; }
        public MatchStats MatchStat { get; set; }

        #endregion Public Properties
    }
}