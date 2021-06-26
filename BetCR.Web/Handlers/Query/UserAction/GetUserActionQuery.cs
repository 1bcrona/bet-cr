using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BetCR.Web.Handlers.Query.UserAction
{

    public class GetUserActionQuery : IRequest<List<Repository.Entity.UserAction>>
    {
        public string ToUserId { get; set; }
        public string FromUserId { get; set; }
        public string ActionType { get; set; }
        public string ActionStatus { get; set; }
    }
  
}
