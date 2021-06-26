using MediatR;

namespace BetCR.Web.Handlers.Query.Match
{
    public class MatchDetailQuery : IRequest<Repository.Entity.Match>
    {
        #region Public Properties

        public string MatchId { get; set; }

        #endregion Public Properties
    }
}