using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.Prediction
{
    public class PredictionQueryHandler : IRequestHandler<PredictionDetailQuery, IEnumerable<Repository.Entity.Match>>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public PredictionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<Repository.Entity.Match>> Handle(PredictionDetailQuery request, CancellationToken cancellationToken)
        {
            var matchRepository = _unitOfWork.GetRepository<Repository.Entity.Match, string>();
            var matches = await matchRepository.FindAsync(f => f.Week == request.Week);

            matches = matches.Where(w => w.HomeTeam != null && w.AwayTeam != null);
            return matches.ToList();
        }

        #endregion Public Methods
    }
}