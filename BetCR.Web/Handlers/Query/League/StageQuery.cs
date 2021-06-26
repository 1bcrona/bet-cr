using MediatR;

namespace BetCR.Web.Handlers.Query.League
{
    public class StageQuery : IRequest<Repository.Entity.Stage>
    {
        #region Public Properties

        public string StageId { get; set; }

        #endregion Public Properties
    }
}