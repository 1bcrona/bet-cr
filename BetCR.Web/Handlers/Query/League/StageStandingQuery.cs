using MediatR;

namespace BetCR.Web.Handlers.Query.League
{
    public class StageStandingQuery : IRequest<Repository.Entity.StageStanding>
    {
        #region Public Properties

        public string StageId { get; set; }

        #endregion Public Properties
    }
}