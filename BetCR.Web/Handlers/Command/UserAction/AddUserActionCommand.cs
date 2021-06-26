using MediatR;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class AddUserActionCommand : IRequest<Repository.Entity.UserAction>
    {
        #region Public Properties

        public string ActionObject { get; set; }
        public string ActionStatus { get; set; }
        public string ActionType { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        #endregion Public Properties
    }
}