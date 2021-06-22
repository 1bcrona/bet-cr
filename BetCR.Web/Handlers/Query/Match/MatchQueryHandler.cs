using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;

namespace BetCR.Web.Handlers.Query.Match
{
    public class MatchDetailQueryHandler : IRequestHandler<MatchDetailQuery, Repository.Entity.Match>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MatchDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Repository.Entity.Match> Handle(MatchDetailQuery request, CancellationToken cancellationToken)
        {
            var matchRepository = _unitOfWork.GetRepository<Repository.Entity.Match, string>();
            var match = await matchRepository.GetAsync(request.MatchId);

            return match;

        }
    }
}
