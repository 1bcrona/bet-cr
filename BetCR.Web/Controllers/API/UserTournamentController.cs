using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Web.Controllers.API.Model;
using BetCR.Web.Handlers.Command;
using BetCR.Web.Handlers.Query.UserTournament;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetCR.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class UserTournamentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _accessor;

        public UserTournamentController(IMediator mediator, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            _accessor = accessor;
        }

        [Authorize]
        [HttpPost]
        [Route("searchuser")]
        public async Task<IActionResult> SearchTournamentUser(SearchUserTournamentModel model)
        {
            var response = new ResponseModel<List<GetUserTournamentSearchUserResponseModel>>();

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

            var result = await _mediator.Send(new GetUserTournamentSearchUserQuery() { Email = model.Email, TournamentId = model.TournamentId, Id = model.Id });

            response.Data = result;
            response.Result = "Success";
            return Ok(response);
        }
    }
}
