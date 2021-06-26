using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class RespondUserTournamentModel
    {
        public string TournamentId { get; set; }

        public bool Response { get; set; }

        public string InvitationId { get; set; }
    }
}
