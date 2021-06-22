using BetCR.Repository.Entity.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class User : EntityBase<string>
    {
        #region Public Properties

        public string Email { get; set; }

        public override string Id { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        [IgnoreDataMember]
        public ICollection<UserMatchBet> UserMatchBets { get; set; }


        #endregion Public Properties
    }
}