using BetCR.Web.Controllers.Base;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using BetCR.Caching.Interface;

namespace BetCR.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly ICache _cache;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor, IMediator mediator, ICache cache) : base(accessor, mediator, cache)
        {
            _logger = logger;
            _mediator = mediator;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "home";
            await Task.CompletedTask;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion Public Methods
    }
}