using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class UpdateUserActionCommand : IRequest<Repository.Entity.UserAction>
    {
        public string Id { get; set; }
        public string ActionStatus { get; set; }
        public string ActionResult { get; set; }

    }
}
