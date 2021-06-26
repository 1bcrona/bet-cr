using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.League.Handler
{
    public class StageQueryHandler : IRequestHandler<StageQuery, Stage>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public StageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Stage> Handle(StageQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Stage, string>();

            var stage = await repository.GetAsync(request.StageId);

            return stage;
        }

        #endregion Public Methods
    }
}