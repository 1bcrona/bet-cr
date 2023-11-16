using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query.League;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Caching.Interface;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class LeagueController : BaseController
    {
        #region Private Fields

        private readonly ILogger<LeagueController> _logger;
        private readonly ICache _cache;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public LeagueController(IMediator mediator, ILogger<LeagueController> logger, IHttpContextAccessor accessor, ICache cache) : base(accessor, mediator, cache)
        {
            _mediator = mediator;
            _logger = logger;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        [HttpGet]
        [Route("stages/{stageId?}")]
        public async Task<IActionResult> Stage([FromRoute] string stageId)
        {
            var result = await _mediator.Send(new StageQuery {StageId = stageId});
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("stages/{leagueId?}")]
        public async Task<IActionResult> Stages([FromRoute] string leagueId)
        {
            var result = await _mediator.Send(new LeagueStageQuery {LeagueId = leagueId});
            return Ok(result.ToList());
        }

        [Authorize]
        [HttpGet]
        [Route("stages/{stageId?}/standings")]
        public async Task<IActionResult> StageStandings([FromRoute] string stageId)
        {
            var result = await _mediator.Send(new StageStandingQuery {StageId = stageId});
            return PartialView("League/_LeagueStanding", result);
        }

        #endregion Public Methods
    }
}