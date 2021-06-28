using System.Collections.Generic;
using System.Threading.Tasks;
using BetCR.Caching.Interface;
using BetCR.Repository.Entity;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query.Tournament;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class TournamentController : BaseController
    {
        #region Private Fields

        private readonly ILogger<PredictionController> _logger;
        private readonly ICache _cache;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public TournamentController(IMediator mediator, ILogger<PredictionController> logger, IHttpContextAccessor accessor, ICache cache) : base(accessor, mediator, cache)
        {
            _mediator = mediator;
            _logger = logger;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        [Route("{tournamentId}/standings")]

        public async Task<IActionResult> GetTournamentStandings(string tournamentId)
        {

            var result = await _mediator.Send(new GetTournamentStandingsQuery() { TournamentId = tournamentId });

            ViewBag.Title = "Standings";
            return View("Tournament/Standings", result);

        }
        #endregion Public Methods
    }
}