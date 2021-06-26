using System.Diagnostics;
using System.Threading.Tasks;
using BetCR.Services.External;
using BetCR.Services.External.Elenasport;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor, IMediator mediator) : base(accessor, mediator)
        {
            _logger = logger;
            _mediator = mediator;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
