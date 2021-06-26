using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.Match
{
    public class MatchDetailQueryHandler : IRequestHandler<MatchDetailQuery, Repository.Entity.Match>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public MatchDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Repository.Entity.Match> Handle(MatchDetailQuery request, CancellationToken cancellationToken)
        {
            var matchRepository = _unitOfWork.GetRepository<Repository.Entity.Match, string>();
            var match = await matchRepository.GetAsync(request.MatchId);

            return match;
        }

        #endregion Public Methods
    }
}