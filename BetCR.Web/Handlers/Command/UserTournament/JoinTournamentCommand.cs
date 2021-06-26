using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class JoinTournamentCommand : IRequest<Tournament>
    {
        #region Public Properties

        public string InviteId { get; set; }
        public string TournamentId { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}