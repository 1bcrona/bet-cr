using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Repository.ValueObject;
using BetCR.Web.Handlers.Query.UserAction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BetCR.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMediator _mediator;

        public BaseController(IHttpContextAccessor accessor, IMediator mediator)
        {
            _accessor = accessor;
            _mediator = mediator;
        }
        public override async void OnActionExecuting(ActionExecutingContext
            filterContext)
        {
            ViewBag.Claims = _accessor.HttpContext?.User.Claims.ToList();
            ViewBag.UserId = (_accessor.HttpContext?.User.Claims.ToList() ?? new List<Claim>()).FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewBag.Invitations = (await _mediator.Send(new GetUserActionQuery() { ActionStatus = UserActionStatus.WAITING_FOR_REPLY, ToUserId = ViewBag.UserId })).ToList();

        }
    }

}
