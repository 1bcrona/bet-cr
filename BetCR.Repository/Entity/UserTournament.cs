using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class UserTournament : BaseEntity<string>
    {
        #region Private Fields

        private Tournament _tournament;
        private User _user;

        #endregion Private Fields

        #region Public Constructors

        public UserTournament()
        {
        }

        public UserTournament(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Tournament Tournament
        {
            get => LazyLoader.Load(this, ref _tournament);
            set => _tournament = value;
        }

        public User User
        {
            get => LazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        #endregion Public Properties
    }
}