﻿using MediatR;
using System.Collections.Generic;
using BetCR.Web.Controllers.API.Model.Response;

namespace BetCR.Web.Handlers.Query.UserTournament
{
    public class GetUserTournamentSearchUserQuery : IRequest<List<GetUserTournamentSearchUserResponseModel>>
    {
        #region Public Properties

        public string Email { get; set; }
        public string Id { get; set; }

        public string TournamentId { get; set; }

        #endregion Public Properties
    }
}