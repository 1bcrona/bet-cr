using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class TournamentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PredictionController> _logger;

        public TournamentController(IMediator mediator, ILogger<PredictionController> logger, IHttpContextAccessor accessor) : base(accessor, mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
