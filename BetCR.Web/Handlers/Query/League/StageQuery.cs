using MediatR;

namespace BetCR.Web.Handlers.Query.Match
{
    public class StageQuery : IRequest<Repository.Entity.Stage>
    {
        public string StageId { get; set; }
    }
}