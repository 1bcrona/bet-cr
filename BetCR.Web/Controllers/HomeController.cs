using System.Diagnostics;
using System.Threading.Tasks;
using BetCR.Services.External;
using BetCR.Services.External.Elenasport;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor) : base(accessor)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "home";
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
