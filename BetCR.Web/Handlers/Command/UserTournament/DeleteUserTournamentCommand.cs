using BetCR.Repository.Entity;
using MediatR;
using System.Collections.Generic;

namespace BetCR.Web.Handlers.Command.UserTournamentRel
{
    public class DeleteUserTournamentCommand : IRequest<List<Tournament>>
    {
        #region Public Properties

        public string TournamentId { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}