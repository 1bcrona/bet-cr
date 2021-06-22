using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;

namespace BetCR.Web.Handlers.Query.Prediction
{
    public class PredictionQueryHandler : IRequestHandler<PredictionDetailQuery, IEnumerable<Repository.Entity.Match>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PredictionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Repository.Entity.Match>> Handle(PredictionDetailQuery request, CancellationToken cancellationToken)
        {
            var matchRepository = _unitOfWork.GetRepository<Repository.Entity.Match, string>();
            var matches = await matchRepository.FindAsync(f => f.Week == request.Week);

            matches = matches.Where(w => w.HomeTeam != null && w.AwayTeam != null);
            return matches.ToList();

        }
    }
}
