using MediatR;

namespace BetCR.Web.Handlers.Query.League
{
    public class StageQuery : IRequest<Repository.Entity.Stage>
    {
        public string StageId { get; set; }
    }
}