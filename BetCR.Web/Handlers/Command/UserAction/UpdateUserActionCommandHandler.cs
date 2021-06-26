using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserAction
{
    public class UpdateUserActionCommandHandler : IRequestHandler<UpdateUserActionCommand, Repository.Entity.UserAction>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserActionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Repository.Entity.UserAction> Handle(UpdateUserActionCommand request, CancellationToken cancellationToken)
        {
            var userActionRepository = _unitOfWork.GetRepository<Repository.Entity.UserAction, string>();
            var invite = await userActionRepository.GetAsync(request.Id);
            if (invite != null)
            {
                invite.ActionResult = request.ActionResult;
                invite.ActionStatus = request.ActionStatus;
                await userActionRepository.UpsertAsync(invite);
            }

            await _unitOfWork.SaveChangesAsync();

            return invite;
        }
    }
}