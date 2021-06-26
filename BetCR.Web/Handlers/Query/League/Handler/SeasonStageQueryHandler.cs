using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.League.Handler
{
    public class SeasonStageQueryHandler : IRequestHandler<LeagueStageQuery, List<Stage>>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SeasonStageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<Stage>> Handle(LeagueStageQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Repository.Entity.Stage, string>();

            var stages = await repository.FindAsync(f => f.LeagueId == request.LeagueId);

            return stages.ToList();
        }

        #endregion Public Methods
    }
}