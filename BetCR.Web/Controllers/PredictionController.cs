using BetCR.Library;
using BetCR.Web.Controllers.Base;
using BetCR.Web.Handlers.Query.Prediction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Caching.Interface;

namespace BetCR.Web.Controllers
{
    [Route("[controller]")]
    public class PredictionController : BaseController
    {
        #region Private Fields

        private readonly ILogger<PredictionController> _logger;
        private readonly ICache _cache;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public PredictionController(IMediator mediator, ILogger<PredictionController> logger, IHttpContextAccessor accessor, ICache cache) : base(accessor, mediator, cache)
        {
            _mediator = mediator;
            _logger = logger;
            _cache = cache;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        [HttpGet]
        [Route("{week?}")]
        public async Task<IActionResult> Index([FromRoute] string week)
        {
            week = string.IsNullOrEmpty(week) ? FixtureHelper.GetWeek(DateTime.Now) : week;
            var result = await _mediator.Send(new PredictionDetailQuery { Week = week });
            ViewBag.Title = "prediction";
            return View(result.ToList());
        }

        #endregion Public Methods
    }
}