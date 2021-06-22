using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Library;
using BetCR.Web.Handlers;
using BetCR.Web.Handlers.Query.Match;
using BetCR.Web.Handlers.Query.Prediction;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class LeagueController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LeagueController> _logger;

        public LeagueController(IMediator mediator, ILogger<LeagueController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }



        [Authorize]
        [HttpGet]
        [Route("stages/{stageId?}/standings")]
        public async Task<IActionResult> StageStandings([FromRoute] string stageId)
        {

            var result = await _mediator.Send(new StageStandingQuery() { StageId = stageId });
            return PartialView("League/_LeagueStanding", result);
        }

        [Authorize]
        [HttpGet]
        [Route("stages/{stageId?}")]
        public async Task<IActionResult> Stage([FromRoute] string stageId)
        {

            var result = await _mediator.Send(new StageQuery() { StageId = stageId });
            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        [Route("stages/{leagueId?}")]
        public async Task<IActionResult> Stages([FromRoute] string leagueId)
        {

            var result = await _mediator.Send(new LeagueStageQuery() { LeagueId = leagueId });
            return Ok(result.ToList());
        }





    }
}
