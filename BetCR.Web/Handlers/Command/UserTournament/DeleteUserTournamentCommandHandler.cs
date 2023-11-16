using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class DeleteUserTournamentCommandHandler : IRequestHandler<DeleteUserTournamentCommand, List<Tournament>>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public DeleteUserTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<Tournament>> Handle(DeleteUserTournamentCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync(cancellationToken);

            var repository = _unitOfWork.GetRepository<Repository.Entity.UserTournament, string>();

            var isUserRegisteredToTournament = (await repository.FindAsync(w => w.User.Id == request.UserId && w.Tournament.Id == request.TournamentId && w.Active == 1)).FirstOrDefault();

            if (isUserRegisteredToTournament == null)
            {
                throw new ApiException() { ErrorCode = "USER_NOT_REGISTERED_TO_TOURNAMENT", ErrorMessage = "User is not registered to this tournament" };
            }

            isUserRegisteredToTournament.Active = 0;
            await repository.UpdateAsync(isUserRegisteredToTournament);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);
            var tournaments = await repository.FindAsync(f => f.Active == 1 && f.User.Id == request.UserId);
            return tournaments.Select(s => s.Tournament).ToList();
        }

        #endregion Public Methods
    }
}