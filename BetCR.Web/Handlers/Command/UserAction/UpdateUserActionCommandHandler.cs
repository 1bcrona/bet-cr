using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class UpdateUserActionCommandHandler : IRequestHandler<UpdateUserActionCommand, Repository.Entity.UserAction>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public UpdateUserActionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Repository.Entity.UserAction> Handle(UpdateUserActionCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync(cancellationToken);
            var userActionRepository = _unitOfWork.GetRepository<Repository.Entity.UserAction, string>();
            var invite = await userActionRepository.GetAsync(request.Id);
            if (invite != null)
            {
                invite.ActionResult = request.ActionResult;
                invite.ActionStatus = request.ActionStatus;
                await userActionRepository.UpsertAsync(invite);
                await _unitOfWork.SaveChangesAsync();
            }

            await transaction.CommitAsync(cancellationToken);
            return invite;
        }

        #endregion Public Methods
    }
}