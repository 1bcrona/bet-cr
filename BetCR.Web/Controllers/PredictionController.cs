using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Library;
using BetCR.Web.Handlers;
using BetCR.Web.Handlers.Query.Prediction;
using BetCR.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class PredictionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PredictionController> _logger;

        public PredictionController(IMediator mediator, ILogger<PredictionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }



        [Authorize]
        [HttpGet]
        [Route("{week?}")]
        public async Task<IActionResult> Index([FromRoute] string week)
        {

            week = string.IsNullOrEmpty(week) ? FixtureHelper.GetWeek(DateTime.Now) : week;
            var result = await _mediator.Send(new PredictionDetailQuery() { Week = week });
            ViewBag.Title = "prediction";
            return View(result.ToList());
        }


    }
}
