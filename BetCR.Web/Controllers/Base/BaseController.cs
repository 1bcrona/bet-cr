using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BetCR.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _accessor;

        public BaseController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public override void OnActionExecuting(ActionExecutingContext
            filterContext)
        {
            ViewBag.Claims = _accessor.HttpContext?.User.Claims.ToList();
            ViewBag.UserId = (_accessor.HttpContext?.User.Claims.ToList() ?? new List<Claim>()).FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }

}
