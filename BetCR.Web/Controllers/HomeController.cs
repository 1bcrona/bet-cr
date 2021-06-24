using System.Diagnostics;
using System.Threading.Tasks;
using BetCR.Services.External;
using BetCR.Services.External.Elenasport;
using BetCR.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElenaFetcherService _elenaFetcherService;

        public HomeController(ILogger<HomeController> logger, IElenaFetcherService elenaFetcherService)
        {
            _logger = logger;
            _elenaFetcherService = elenaFetcherService;
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
