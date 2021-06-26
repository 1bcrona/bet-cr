using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Web.Controllers.API.Model;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class AddUserActionCommandHandler : IRequestHandler<AddUserActionCommand, Repository.Entity.UserAction>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public AddUserActionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Repository.Entity.UserAction> Handle(AddUserActionCommand request, CancellationToken cancellationToken)
        {
            var userActionRepository = _unitOfWork.GetRepository<Repository.Entity.UserAction, string>();
            var userRepository = _unitOfWork.GetRepository<Repository.Entity.User, string>();

            var isExisting = await userActionRepository.FindAsync(f =>
                f.Active == 1
                & f.ActionStatus == request.ActionStatus
                & f.ActionType == request.ActionType
                && f.FromUser.Id == request.FromUserId
                && f.ActionObject == request.ActionObject
                && f.ToUser.Id == request.ToUserId);

            var fromUser = await userRepository.GetAsync(request.FromUserId);

            if (fromUser == null)
            {
                throw new ApiException() { ErrorCode = "FROM_USER_NOT_FOUND", ErrorMessage = "Specified User Not Found", StatusCode = 500 };
            }
            var toUser = await userRepository.GetAsync(request.ToUserId);

            if (toUser == null)
            {
                throw new ApiException() { ErrorCode = "FROM_USER_NOT_FOUND", ErrorMessage = "Specified User Not Found", StatusCode = 500 };
            }

            if (isExisting.Any())
            {
                throw new ApiException() { ErrorCode = "INVITATION_ALREADY_SENT", ErrorMessage = "Invitation has already been sent", StatusCode = 500 };
            }

            var added = await userActionRepository.AddAsync(new Repository.Entity.UserAction()
            {
                FromUser = fromUser,
                ToUser = toUser,
                Id = Guid.NewGuid().ToString("D"),
                ActionStatus = request.ActionStatus,
                ActionObject = request.ActionObject,
                ActionDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                ActionType = request.ActionType
            });

            await _unitOfWork.SaveChangesAsync();
            return added;
        }

        #endregion Public Methods
    }
}