using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class User : BaseEntity<string>
    {
        #region Private Fields

        private ICollection<UserTournament> _userTournameRels;

        #endregion Private Fields

        #region Public Constructors

        public User()
        {
        }

        public User(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public string DateOfBirth { get; set; }
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string FullName => string.Join(" ", Firstname, Surname);

        public override string Id { get; set; }

        public string Password { get; set; }
        public string Surname { get; set; }

        [IgnoreDataMember] public ICollection<UserMatchBet> UserMatchBets { get; set; }

        public ICollection<UserTournament> UserTournameRels
        {
            get => LazyLoader.Load(this, ref _userTournameRels);
            set => _userTournameRels = value;
        }

        #endregion Public Properties
    }
}