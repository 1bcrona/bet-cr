using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class UserTournameRel : EntityBase<string>
    {
        private User _user;
        private Tournament _tournament;

        public UserTournameRel()
        {
        }
        public UserTournameRel(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

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
    }
}
