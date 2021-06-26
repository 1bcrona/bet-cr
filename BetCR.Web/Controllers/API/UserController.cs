using BetCR.Library;
using BetCR.Repository.Entity;
using BetCR.Services.Base;
using BetCR.Web.Controllers.API.Model;
using BetCR.Web.Handlers.Command;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetCR.Web.Handlers.Command.UserTournamentRel;
using BetCR.Web.Handlers.Query.Tournament;

namespace BetCR.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        #region Private Fields

        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<API.UserController> _logger;
        private readonly IMediator _mediator;
        private IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public UserController(ILogger<API.UserController> logger, IUserService userService, IMediator mediator, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _userService = userService;
            _mediator = mediator;
            _accessor = accessor;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        [Authorize]
        [Route("Tournament")]
        public async Task<IActionResult> UserTournament()
        {

            var response = new ResponseModel<GetUserTournamentResponseModel>();

            try
            {
                var userId = _accessor.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
                var result = await _mediator.Send(new GetUserTournamentQuery
                {
                    UserId = userId
                });


                response.Data = result;
                response.Result = "Tournament Get Operation Succeeded";
            }
            catch (Exception ex)
            {
                response.Result = "Tournament Get Operation Failed";
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);




        }


        [HttpDelete]
        [Authorize]
        [Route("Tournament/{tournamentId}")]
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

            try
            {
                await _mediator.Send(new DeleteUserTournamentCommand { TournamentId = tournamentId, UserId = userId });
                response.Data = "Tournament Leave Operation Successfull";
                response.Result = "Tournament Delete Operation Successful";
            }
            catch (Exception ex)
            {
                response.Result = "Tournament Delete Operation Failed";
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = new ResponseModel<List<object>>();

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

            model.Email = model.Email.NormalizeString();

            var isUser = await _userService.GetUser(model.Email);

            if (isUser == null)
            {
                response.ErrorMessage = "User or Password is not correct";
                response.Result = "Login Failed";
                return StatusCode(500, response);
            }

            if (isUser.Password == EncryptionHelper.MD5Hash(model.Password))
            {
                var userTournaments = isUser.UserTournameRels
                    .Where(w => w.Tournament.IsStill && w.Active == 1 && w.Tournament.Active == 1)
                    .OrderBy(o => o.Tournament.TournamentEndDateEpoch).Select(s => s.Tournament).ToList();

                List<Claim> userClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, isUser.Id),
                    new(ClaimTypes.Name, String.Join(" ", isUser.Firstname, isUser.Surname)),
                    new(ClaimTypes.GivenName, isUser.Firstname),
                    new(ClaimTypes.Surname, isUser.Surname),
                    new(ClaimTypes.Email, isUser.Email),
                    new (ClaimTypes.IsPersistent, model.IsRememberMe.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.IsRememberMe
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                response.ErrorMessage = null;
                response.Result = "Login Successful";
                response.ReturnUrl = "/";
                return Ok(response);
            }
            else
            {
                response.ErrorMessage = "User or Password is not correct";
                response.Result = "Login Failed";
                response.ReturnUrl = "/User/Login";
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var response = new ResponseModel<object>();

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

            model.Email = model.Email.NormalizeString();
            var existingUser = await _userService.GetUser(model.Email);

            if (existingUser == null)
            {
                existingUser = new User
                {
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Password = EncryptionHelper.MD5Hash(model.Password),
                    Surname = model.Surname,
                    Id = Guid.NewGuid().ToString("D")
                };
                await _userService.SaveUser(existingUser);

                await Login(new LoginModel { Email = existingUser.Email, IsRememberMe = true, Password = model.Password });
                response.Result = "User registration successful.";
                response.ReturnUrl = "/";
                return Ok(response);
            }
            else
            {
                response.Result = "User registration fail. Existing user found with this mail address";
                response.ErrorMessage = "User registration fail. Existing user found with this mail address";
                return StatusCode(500, response);
            }
        }

        #endregion Public Methods
    }
}