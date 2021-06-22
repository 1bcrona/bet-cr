using MediatR;

namespace BetCR.Web.Handlers.Query.Match
{
    public class MatchDetailQuery: IRequest<Repository.Entity.Match>
    {
        public string MatchId { get; set; }
    }
}
