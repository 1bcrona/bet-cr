using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Library.Tracking;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class JoinTournamentCommandHandler : IRequestHandler<JoinTournamentCommand, Tournament>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public JoinTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Tournament> Handle(JoinTournamentCommand request, CancellationToken cancellationToken)
        {
            var userTournamentRepository = _unitOfWork.GetRepository<Repository.Entity.UserTournament, string>();
            var tournamentRepository = _unitOfWork.GetRepository<Repository.Entity.Tournament, string>();
            var userRepository = _unitOfWork.GetRepository<Repository.Entity.User, string>();
            var isExisting = await userTournamentRepository.FindAsync(f => f.Active == 1 && f.User.Id == request.UserId && f.Tournament.Id == request.TournamentId);

            if (isExisting.Any())
            {
                throw new ApiException() { ErrorCode = "USER_ALREADY_JOINED", ErrorMessage = "User has already joined to this tournament", StatusCode = 500 };
            }
            else
            {
                var user = await userRepository.GetAsync(request.UserId);

                if (user == null)
                {
                    throw new ApiException() { ErrorCode = "USER_NOT_FOUND", ErrorMessage = "User cannot be found", StatusCode = 500 };
                }

                var tournament = await tournamentRepository.GetAsync(request.TournamentId);
                if (tournament == null)
                {
                    throw new ApiException() { ErrorCode = "TOURNAMENT_NOT_FOUND", ErrorMessage = "Tournament cannot be found", StatusCode = 500 };
                }

                var userTournament = new Repository.Entity.UserTournament
                {
                    Id = Guid.NewGuid().ToString("D"),
                    User = user,
                    Tournament = tournament,
                    Active = 1
                };

                await userTournamentRepository.AddAsync(userTournament);

                if (request.InviteId != null)
                {
                    var userActionRepository = _unitOfWork.GetRepository<Repository.Entity.UserAction, string>();
                    var invite = await userActionRepository.GetAsync(request.InviteId);
                    if (invite != null)
                    {
                        invite.ActionResult = "OK";
                        invite.ActionStatus = "RESPOND";
                        await userActionRepository.UpsertAsync(invite);
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                return userTournament.Tournament;
            }
        }

        #endregion Public Methods
    }
}