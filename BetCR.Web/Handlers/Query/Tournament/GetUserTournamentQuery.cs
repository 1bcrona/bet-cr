using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Web.Controllers.API.Model;
using MediatR;

namespace BetCR.Web.Handlers.Query.Tournament
{
    public class GetUserTournamentQuery : IRequest<GetUserTournamentResponseModel>
    {
        public string UserId { get; set; }

        public bool IsPublic { get; set; }
    }
}
