using BetCR.Repository.Entity.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class User : EntityBase<string>
    {

        public User()
        {

        }

        public User(ILazyLoader lazyLoader) : base(lazyLoader)
        {

        }
        private ICollection<UserTournameRel> _userTournameRels;

        #region Public Properties

        public string Email { get; set; }

        public override string Id { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        [IgnoreDataMember]
        public ICollection<UserMatchBet> UserMatchBets { get; set; }

        public ICollection<UserTournameRel> UserTournameRels
        {
            get => LazyLoader.Load(this, ref _userTournameRels);
            set => _userTournameRels = value;
        }

        #endregion Public Properties
    }
}