using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity
{
    public class UserAction : EntityBase<string>
    {
        private User _fromUser;
        private User _toUser;

        public UserAction() { }
        public UserAction(ILazyLoader lazyLoader) : base(lazyLoader) { }

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

        public string ActionType { get; set; }

        public string ActionObject { get; set; }
        public string ActionStatus { get; set; }

        public string ActionResult { get; set; }


        public long ActionDateEpoch { get; set; }

        public CustomDateTime ActionDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(ActionDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }
    }
}
