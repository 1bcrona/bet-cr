using System.Collections.Generic;
using BetCR.Web.Controllers.API.Model;
using MediatR;

namespace BetCR.Web.Handlers.Query.UserTournament
{
    public class GetUserTournamentSearchUserQuery : IRequest<List<GetUserTournamentSearchUserResponseModel>>
    {
        public string Email { get; set; }
        public string Id { get; set; }

        public string TournamentId { get; set; }
    }
}
