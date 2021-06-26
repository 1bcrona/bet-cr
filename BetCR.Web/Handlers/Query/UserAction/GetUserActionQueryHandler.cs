using BetCR.Library;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Query.UserAction
{
    public class GetUserActionQueryHandler : IRequestHandler<GetUserActionQuery, List<Repository.Entity.UserAction>>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public GetUserActionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<Repository.Entity.UserAction>> Handle(GetUserActionQuery request, CancellationToken cancellationToken)
        {
            var userActionRepository = _unitOfWork.GetRepository<Repository.Entity.UserAction, string>();

            var predicate = PredicateBuilder.True<Repository.Entity.UserAction>();
            if (request.FromUserId != null)
            {
                predicate = predicate.And(p => p.FromUser.Id == request.FromUserId);
            }

            if (request.ToUserId != null)
            {
                predicate = predicate.And(p => p.ToUser.Id == request.ToUserId);
            }

            if (request.ActionType != null)
            {
                predicate = predicate.And(p => p.ActionType == request.ActionType);
            }

            if (request.ActionStatus != null)
            {
                predicate = predicate.And(p => p.ActionStatus == request.ActionStatus);

            }
            predicate = predicate.And(p => p.Active == 1);

            var userActions = await userActionRepository.FindAsync(predicate);

            return userActions.ToList();
        }

        #endregion Public Methods
    }
}