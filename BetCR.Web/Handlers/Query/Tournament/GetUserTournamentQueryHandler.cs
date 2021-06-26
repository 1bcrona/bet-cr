using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.Tournament
{
    public class GetUserTournamentQueryHandler : IRequestHandler<GetUserTournamentQuery, GetUserTournamentResponseModel>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public GetUserTournamentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<GetUserTournamentResponseModel> Handle(GetUserTournamentQuery request, CancellationToken cancellationToken)
        {
            var tournamentRepository = _unitOfWork.GetRepository<Repository.Entity.UserTournament, string>();

            var tournaments = await tournamentRepository.FindAsync(f => f.Active == 1 && f.User.Id == request.UserId);
            var userTournameRels = tournaments.ToList();
            var allTournaments = userTournameRels.Select(s => s.Tournament).ToList();
            ;
            var userTournaments = new GetUserTournamentResponseModel
            {
                All = allTournaments,
                Current = allTournaments.OrderBy(o => o.TournamentEndDateEpoch).FirstOrDefault()
            };

            return userTournaments;
        }

        #endregion Public Methods
    }
}