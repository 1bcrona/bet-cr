using BetCR.Web.Controllers.API.Model.Response;
using MediatR;

namespace BetCR.Web.Handlers.Query.Tournament
{
    public class GetUserTournamentQuery : IRequest<GetUserTournamentResponseModel>
    {
        #region Public Properties

        public bool IsPublic { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}