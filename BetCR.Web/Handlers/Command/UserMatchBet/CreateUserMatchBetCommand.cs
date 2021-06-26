using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserMatchBet
{
    public class CreateUserMatchBetCommand : IRequest<Match>
    {
        #region Public Properties

        public int AwayTeamScore { get; set; }
        public int HomeTeamScore { get; set; }
        public int Leverage { get; set; }
        public string MatchId { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}