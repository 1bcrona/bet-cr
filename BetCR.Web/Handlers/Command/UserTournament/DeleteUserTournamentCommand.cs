using System.Collections.Generic;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class DeleteUserTournamentCommand : IRequest<List<Tournament>>
    {
        #region Public Properties

        public string TournamentId { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}