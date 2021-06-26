using MediatR;
using System.Collections.Generic;

namespace BetCR.Web.Handlers.Query.UserAction
{
    public class GetUserActionQuery : IRequest<List<Repository.Entity.UserAction>>
    {
        #region Public Properties

        public string ActionStatus { get; set; }
        public string ActionType { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        #endregion Public Properties
    }
}