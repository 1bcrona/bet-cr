using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Services.External;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query;
using BetCR.Web.Handlers.Query.Match;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class MatchController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MatchController> _logger;
        private readonly IHttpContextAccessor _accessor;

        public MatchController(IMediator mediator, ILogger<MatchController> logger, IHttpContextAccessor accessor) : base(accessor)
        {
            _mediator = mediator;
            _logger = logger;
            _accessor = accessor;
        }

        [Authorize]
        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Index(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery { MatchId = id });


            var userId = _accessor.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;

            ViewData["UserBet"] = result.UserMatchBets.FirstOrDefault(w => w.User.Id == userId);

            var x = PartialView("Match/_MatchCard", result);
            return x;
        }

        [Authorize]
        [HttpGet]
        [Route("Pitch/{id?}")]
        public async Task<IActionResult> Pitch(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery { MatchId = id });
            return PartialView("Match/_MatchPitch", result);
        }

        [Authorize]
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery { MatchId = id });
            return PartialView("Match/_MatchDetails", result);
        }


    }
}
