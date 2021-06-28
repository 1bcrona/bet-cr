using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetCR.Repository.Entity
{
    public class UserAction : BaseEntity<string>
    {
        #region Private Fields

        private User _fromUser;
        private User _toUser;

        #endregion Private Fields

        #region Public Constructors

        public UserAction()
        {
        }

        public UserAction(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public CustomDateTime ActionDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(ActionDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        public long ActionDateEpoch { get; set; }

        public string ActionObject { get; set; }

        public string ActionResult { get; set; }

        public string ActionStatus { get; set; }

        public string ActionType { get; set; }

        [ForeignKey("FromUserId")]
        public User FromUser
        {
            get => LazyLoader.Load(this, ref _fromUser);
            set => _fromUser = value;
        }

        [ForeignKey("ToUserId")]
        public User ToUser
        {
            get => LazyLoader.Load(this, ref _toUser);
            set => _toUser = value;
        }

        #endregion Public Properties
    }
}