using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Handlers.Query.Match;
using MediatR;

namespace BetCR.Web.Handlers.Query.League.Handler
{
    public class StageQueryHandler : IRequestHandler<StageQuery, Stage>
    {
        private readonly IUnitOfWork _unitOfWork;


        public StageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Stage> Handle(StageQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Stage, string>();

            var stage = await repository.GetAsync(request.StageId);

            return stage;
        }
    }
}
