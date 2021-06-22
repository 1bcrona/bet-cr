using System;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class UserController : Controller
    {
        #region Private Fields

        private readonly ILogger<UserController> _logger;
        private IHttpContextAccessor _accessor;

        #endregion Private Fields

        #region Public Constructors

        public UserController(ILogger<UserController> logger, IUserService userService, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "/");
        }

        [HttpGet]
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