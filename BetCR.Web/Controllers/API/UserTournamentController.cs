using BetCR.Library;
using BetCR.Repository.Entity;
using BetCR.Repository.ValueObject;
using BetCR.Web.Controllers.API.Model;
using BetCR.Web.Controllers.API.Validator;
using BetCR.Web.Handlers.Command.UserAction;
using BetCR.Web.Handlers.Command.UserTournament;
using BetCR.Web.Handlers.Query.UserTournament;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Web.Controllers.API.Model.Request;
using BetCR.Web.Controllers.API.Model.Response;

namespace BetCR.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class UserTournamentController : Controller
    {
        #region Private Fields

        private readonly IHttpContextAccessor _accessor;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public UserTournamentController(IMediator mediator, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            _accessor = accessor;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpDelete]
        [Authorize]
        [Route("{tournamentId}")]
        public async Task<IActionResult> DeleteUserTournament(string tournamentId)
        {
            var response = new ResponseModel<string>();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .SelectMany(s => s.Select(s1 => s1.Exception?.ToString() ?? s1.ErrorMessage))
                    .ToList();

                var combinedErrors = String.Join("/r/n", errors);
                response.Result = combinedErrors;
                response.Result = combinedErrors;

                return BadRequest(response);
            }

            var userId = _accessor.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteUserTournamentCommand { TournamentId = tournamentId, UserId = userId });
            response.Data = "Tournament Leave Operation Successfull";
            response.Result = "Tournament Delete Operation Successful";

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserToTournamentModel model)
        {
            var response = new ResponseModel<UserAction>();

            model.InviterUserId ??= _accessor.HttpContext?.User.Claims
                .FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var validation = await new InviteUserTournamentModelValidator().ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .SelectMany(s => s.Select(s1 => s1.Exception?.ToString() ?? s1.ErrorMessage))
                    .ToList();

                var combinedErrors = String.Join("/r/n", errors);
                response.Result = combinedErrors;

                return BadRequest(response);
            }

            var result = await _mediator.Send(new AddUserActionCommand()
            {
                ActionObject = model.TournamentId,
                ActionType = UserActionType.TOURNAMENT_INVITE,
                ActionStatus = UserActionStatus.WAITING_FOR_REPLY,
                FromUserId = model.InviterUserId,
                ToUserId = model.InviteeUserId
            });

            response.Result = $"Invitation sent";
            response.Data = result;

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateTournamentModel model)
        {
            var response = new ResponseModel<UserTournament>();

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

            var userId = _accessor.HttpContext?.User.Claims
                .FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var result = await _mediator.Send(new CreateUserTournamentCommand()
            {
                EndDateEpoch = model.EndDateEpoch,
                StartDateEpoch = model.StartDateEpoch,
                TournamentName = model.TournamentName,
                TournamentPassword = EncryptionHelper.MD5Hash(model.Password),
                UserId = userId
            });

            response.Data = result;
            response.Result = "Tournament Successfully Created";
            return Created(string.Empty, response);
        }

        [Authorize]
        [HttpPost]
        [Route("invite/respond")]
        public async Task<IActionResult> RespondInvite([FromBody] RespondUserTournamentModel model)
        {
            var response = new ResponseModel<UserAction>();

            var userId = _accessor.HttpContext?.User.Claims
                .FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var validation = await new RespondUserTournamentModelValidator().ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .SelectMany(s => s.Select(s1 => s1.Exception?.ToString() ?? s1.ErrorMessage))
                    .ToList();

                var combinedErrors = String.Join("/r/n", errors);
                response.Result = combinedErrors;

                return BadRequest(response);
            }

            UserAction action = null;

            if (model.Response)
            {
                await _mediator.Send(new JoinTournamentCommand()
                {
                    InviteId = model.InvitationId,
                    TournamentId = model.TournamentId,
                    UserId = userId
                });
            }
            else
            {
                action = await _mediator.Send(new UpdateUserActionCommand()
                {
                    ActionResult = model.Response.ToString().ToUpperInvariant(),
                    ActionStatus = UserActionStatus.RESPOND,
                    Id = model.InvitationId
                });
            }

            response.Result = $"Operation Completed";
            response.Data = action;

            return Ok(response);
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

        #endregion Public Methods
    }
}