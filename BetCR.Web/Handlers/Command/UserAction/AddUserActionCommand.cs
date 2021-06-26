using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class AddUserActionCommand : IRequest<Repository.Entity.UserAction>
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ActionType { get; set; }

        public string ActionStatus { get; set; }
        public string ActionObject { get; set; }
    }
}
