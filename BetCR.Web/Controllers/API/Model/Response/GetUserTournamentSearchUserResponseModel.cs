using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Repository.Entity;

namespace BetCR.Web.Controllers.API.Model
{
    public class GetUserTournamentSearchUserResponseModel
    {
        public User User { get; set; }

        public bool IsRegisteredToTournament { get; set; }

        public string TournamentId { get; set; }
    }
}
