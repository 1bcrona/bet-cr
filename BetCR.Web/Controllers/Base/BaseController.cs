using BetCR.Repository.ValueObject;
using BetCR.Web.Handlers.Query.UserAction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BetCR.Caching.Interface;
using BetCR.Repository.Entity;
using BetCR.Web.Handlers.Query.Tournament;

namespace BetCR.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        #region Private Fields

        private readonly IHttpContextAccessor _accessor;
        private readonly IMediator _mediator;
        private readonly ICache _cache;

        #endregion Private Fields

        #region Public Constructors

        public BaseController(IHttpContextAccessor accessor, IMediator mediator, ICache cache)
        {
            _accessor = accessor;
            _mediator = mediator;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        public override async void OnActionExecuting(ActionExecutingContext
            filterContext)
        {
            ViewBag.Claims = _accessor.HttpContext?.User.Claims.ToList();
            ViewBag.UserId = (_accessor.HttpContext?.User.Claims.ToList() ?? new List<Claim>()).FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value;

            if (ViewBag.UserId == null) return;

            ViewBag.Invitations = (await _mediator.Send(new GetUserActionQuery() {ActionStatus = UserActionStatus.WAITING_FOR_REPLY, ToUserId = ViewBag.UserId})).ToList();
            var userTournamentResult = await _mediator.Send(new GetUserTournamentQuery {UserId = ViewBag.UserId});
            ViewBag.UserTournaments = userTournamentResult.All;
        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.CurrentTournament = await _cache.Get<string>($"{ViewBag.UserId}_CurrentTournament", null) ??
                                        (ViewBag.UserTournaments as List<Tournament> ?? new List<Tournament>()).OrderBy(f => f.TournamentEndDateEpoch).Select(s => s.Id).FirstOrDefault();
            ;
        }

        #endregion Public Methods
    }
}