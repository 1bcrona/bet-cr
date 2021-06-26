using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Repository.Entity;

namespace BetCR.Web.Controllers.API.Model
{
    public class GetUserTournamentResponseModel
    {
        public List<Tournament> All;

        public Tournament Current;
    }
}
