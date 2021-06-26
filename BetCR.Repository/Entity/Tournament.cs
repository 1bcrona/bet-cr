using System;
using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using BetCR.Repository.ValueObject;

namespace BetCR.Repository.Entity
{
    public class Tournament : EntityBase<string>
    {
        private User _owner;

        public override string Id { get; set; }

        public string TournamentName { get; set; }

        public long TournameStartDateEpoch { get; set; }

        public bool IsPrivate { get; set; }

        public Tournament()
        {

        }

        public Tournament(ILazyLoader lazyLoader) : base(lazyLoader)
        {

        }

        public string TournamentPassword { get; set; }

        [ForeignKey("OwnerUserId")]
        public User Owner
        {
            get => LazyLoader.Load(this, ref _owner);
            set => _owner = value;
        }

        public bool IsStill => TournamentEndDateEpoch > DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public CustomDateTime TournamentStartDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(TournameStartDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        public long TournamentEndDateEpoch { get; set; }

        public CustomDateTime TournamentEndDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(TournamentEndDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        [IgnoreDataMember]
        public ICollection<UserTournament> UserTournameRels { get; set; }

    }

}