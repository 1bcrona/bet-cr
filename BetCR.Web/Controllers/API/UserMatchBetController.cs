using BetCR.Repository.Entity;
using BetCR.Web.Controllers.API.Model;
using BetCR.Web.Handlers.Command.UserMatchBet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class UserMatchBetController : Controller
    {
        #region Private Fields

        private readonly IHttpContextAccessor _accessor;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public UserMatchBetController(IMediator mediator, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            _accessor = accessor;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserMatchBetModel mode)
        {
            var response = new ResponseModel<Match>();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .SelectMany(s => s.Select(s1 => s1.Exception?.ToString() ?? s1.ErrorMessage))
                    .ToList();

                var combinedErrors = String.Join("/r/n", errors);
                response.Result = combinedErrors;

                return BadRequest(response);
            }

            var userId = _accessor.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new CreateUserMatchBetCommand
            {
                AwayTeamScore = mode.AwayTeamScore,
                HomeTeamScore = mode.HomeTeamScore,
                Leverage = mode.Leverage,
                MatchId = mode.MatchId,
                UserId = userId
            });

            response.Data = result;
            response.Result = "Success";
            return Ok(response);
        }

        #endregion Public Methods
    }
}