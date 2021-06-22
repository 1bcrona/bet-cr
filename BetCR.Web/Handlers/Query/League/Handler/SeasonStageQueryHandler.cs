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
    public class SeasonStageQueryHandler : IRequestHandler<LeagueStageQuery, List<Stage>>
    {

        private readonly IUnitOfWork _unitOfWork;


        public SeasonStageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Stage>> Handle(LeagueStageQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Repository.Entity.Stage, string>();

            var stages = await repository.FindAsync(f => f.LeagueId == request.LeagueId);

            return stages.ToList();
        }
    }
}
