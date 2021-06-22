using System.Diagnostics;
using System.Threading.Tasks;
using BetCR.Services.External;
using BetCR.Web.Handlers.Query;
using BetCR.Web.Handlers.Query.Match;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class MatchController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MatchController> _logger;

        public MatchController(IMediator mediator, ILogger<MatchController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Index(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery() { MatchId = id });
            return PartialView("Match/_MatchCard", result);
        }

        [Authorize]
        [HttpGet]
        [Route("Pitch/{id?}")]
        public async Task<IActionResult> Pitch(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery() { MatchId = id });
            return PartialView("Match/_MatchPitch", result);
        }

        [Authorize]
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery() { MatchId = id });
            return PartialView("Match/_MatchDetails", result);
        }


    }
}
