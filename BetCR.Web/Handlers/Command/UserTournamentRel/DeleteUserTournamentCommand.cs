using System.Collections.Generic;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserTournamentRel
{
    public class DeleteUserTournamentCommand : IRequest<List<Tournament>>
    {
        public string UserId { get; set; }
        public string TournamentId { get; set; }

    }
}
