using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.ValueObject;
using BetCR.Services.Base;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query.Tournament;
using BetCR.Web.Handlers.Query.UserAction;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : BaseController
    {
        #region Private Fields

        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private IHttpContextAccessor _accessor;

        #endregion Private Fields

        #region Public Constructors

        public UserController(ILogger<UserController> logger, IMediator mediator, IHttpContextAccessor accessor) : base(accessor, mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _accessor = accessor;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl = "")
        {
            if (_accessor.HttpContext != null)
            {
                var claims = _accessor.HttpContext.User.Claims.ToList();
                if (claims.Count > 0)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "/");
                }
            }

            return View();
        }



        [HttpGet]
        [Authorize]
        [Route("Tournament")]
        public async Task<IActionResult> GetUserTournament()
        {
            var userId = _accessor.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new GetUserTournamentQuery
            {
                UserId = userId
            });

            var invitationResult = await _mediator.Send(new GetUserActionQuery() { ActionType = UserActionType.TOURNAMENT_INVITE, ActionStatus = UserActionStatus.WAITING_FOR_REPLY, ToUserId = userId });

            ViewBag.TournamentInvitations = invitationResult;
            return View("Tournament/Index", result.All);
        }



        [HttpGet]
        [Authorize]
        [Route("ChangeTournament/{tournamentId}")]
        public async Task<IActionResult> ChangeTournament([FromRoute] string tournamentId)
        {

            if (String.IsNullOrEmpty((tournamentId ?? String.Empty).Trim()))
            {
                Login();
            }

            await Task.CompletedTask;
            ViewBag.CurrentTournamentId = tournamentId;
            return RedirectToAction("Index", "/");
        }




        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "/");
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            if (_accessor.HttpContext != null)
            {
                var claims = _accessor.HttpContext.User.Claims.ToList();
                if (claims.Count > 0)
                {
                    return RedirectToAction("Index", "/");
                }
            }

            return View();
        }

        #endregion Public Methods
    }
}