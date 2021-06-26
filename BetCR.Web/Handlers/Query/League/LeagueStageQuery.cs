using BetCR.Repository.Entity;
using MediatR;
using System.Collections.Generic;

namespace BetCR.Web.Handlers.Query.League
{
    public class LeagueStageQuery : IRequest<List<Stage>>
    {
        #region Public Properties

        public string LeagueId { get; set; }

        #endregion Public Properties
    }
}