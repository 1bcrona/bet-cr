using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class UserMatchBet : EntityBase<string>
    {
        #region Private Fields

        private Match _match;

        private User _user;

        #endregion Private Fields

        #region Public Constructors

        public UserMatchBet()
        {
        }

        public UserMatchBet(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public int AwayTeamScore { get; set; }
        public int HomeTeamScore { get; set; }

        public int Leverage { get; set; }
        public int UserBetPointDefault { get; set; }
        public override string Id { get; set; }

        public Match Match
        {
            get => LazyLoader.Load(this, ref _match);
            set => _match = value;
        }

        public int? ProcessState { get; set; }

        public User User
        {
            get => LazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        public int? UserBetPoint { get; set; }

        #endregion Public Properties
    }
}