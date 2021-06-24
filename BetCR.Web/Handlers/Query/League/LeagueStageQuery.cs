using System.Collections.Generic;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Query.League
{
    public class LeagueStageQuery : IRequest<List<Stage>>
    {
        public string LeagueId { get; set; }
    }
}