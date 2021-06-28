using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity
{
    public class Tournament : EntityBase<string>
    {
        #region Private Fields

        private User _owner;

        #endregion Private Fields

        #region Public Constructors

        public Tournament()
        {
        }

        public Tournament(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Id { get; set; }

        public bool IsPrivate { get; set; }

        [NotMapped]
        public bool IsStill
        {
            get { return TournamentEndDateEpoch > DateTimeOffset.UtcNow.ToUnixTimeSeconds(); }
        }

        [ForeignKey("OwnerUserId")]
        public User Owner
        {
            get => LazyLoader.Load(this, ref _owner);
            set => _owner = value;
        }

        public CustomDateTime TournamentEndDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(TournamentEndDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        public long TournamentEndDateEpoch { get; set; }
        public string TournamentName { get; set; }

        public string TournamentPassword { get; set; }

        public CustomDateTime TournamentStartDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(TournameStartDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        public long TournameStartDateEpoch { get; set; }

        [IgnoreDataMember]
        public ICollection<UserTournament> UserTournameRels { get; set; }

        #endregion Public Properties
    }
}