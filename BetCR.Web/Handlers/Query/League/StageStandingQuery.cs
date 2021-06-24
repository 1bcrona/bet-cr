using MediatR;

namespace BetCR.Web.Handlers.Query.League
{
    public class StageStandingQuery : IRequest<Repository.Entity.StageStanding>
    {
        public string StageId { get; set; }
    }
}
