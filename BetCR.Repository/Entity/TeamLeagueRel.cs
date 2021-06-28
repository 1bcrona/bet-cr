using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class TeamLeagueRel : BaseEntity<string>
    {
        #region Private Fields

        private League _league;

        private Team _Team;

        #endregion Private Fields

        #region Public Constructors

        public TeamLeagueRel()
        {
        }

        public TeamLeagueRel(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Id { get; set; }

        public League League
        {
            get => LazyLoader.Load(this, ref _league);
            set => _league = value;
        }

        public Team Team
        {
            get => LazyLoader.Load(this, ref _Team);
            set => _Team = value;
        }

        #endregion Public Properties
    }
}