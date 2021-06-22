using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Command
{
    public class CreateUserMatchBetCommand : IRequest<Match>
    {
        public string UserId { get; set; }
        public string MatchId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int Leverage { get; set; }
    }
}
