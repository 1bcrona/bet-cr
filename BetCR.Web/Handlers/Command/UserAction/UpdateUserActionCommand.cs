using MediatR;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class UpdateUserActionCommand : IRequest<Repository.Entity.UserAction>
    {
        #region Public Properties

        public string ActionResult { get; set; }
        public string ActionStatus { get; set; }
        public string Id { get; set; }

        #endregion Public Properties
    }
}