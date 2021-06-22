using System.Collections.Generic;
using BetCR.Repository.Entity;
using MediatR;

namespace BetCR.Web.Handlers.Query.Match
{
    public class LeagueStageQuery : IRequest<List<Stage>>
    {
        public string LeagueId { get; set; }
    }
}