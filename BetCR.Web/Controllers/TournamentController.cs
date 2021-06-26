using BetCR.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetCR.Web.Controllers
{
    public class TournamentController : BaseController
    {
        #region Private Fields

        private readonly ILogger<PredictionController> _logger;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public TournamentController(IMediator mediator, ILogger<PredictionController> logger, IHttpContextAccessor accessor) : base(accessor, mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }

        #endregion Public Methods
    }
}