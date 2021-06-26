using System.Collections.Generic;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class JoinTournamentCommand : IRequest<Tournament>
    {
        public string UserId { get; set; }
        public string TournamentId { get; set; }

        public string InviteId { get; set; }
    }
}
