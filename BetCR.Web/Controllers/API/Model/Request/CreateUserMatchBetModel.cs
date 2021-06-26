using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class CreateUserMatchBetModel
    {
        public string MatchId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int Leverage { get; set; }
    }
}
