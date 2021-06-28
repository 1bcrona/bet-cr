using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query.Match;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Caching.Interface;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class MatchController : BaseController
    {
        #region Private Fields

        private readonly IHttpContextAccessor _accessor;
        private readonly ICache _cache;
        private readonly ILogger<MatchController> _logger;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public MatchController(IMediator mediator, ILogger<MatchController> logger, IHttpContextAccessor accessor,ICache cache) : base(accessor, mediator,cache)
        {
            _mediator = mediator;
            _logger = logger;
            _accessor = accessor;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            var result = await _mediator.Send(new MatchDetailQuery { MatchId = id });
            return PartialView("Match/_MatchDetails", result);
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

        #endregion Public Methods
    }
}