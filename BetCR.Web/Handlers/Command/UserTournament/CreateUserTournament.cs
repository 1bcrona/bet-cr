using System;
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
    public class CreateUserTournamentCommand : IRequest<Repository.Entity.UserTournament>
    {

        public string TournamentName { get; set; }
        public string TournamentPassword { get; set; }
        public long StartDateEpoch { get; set; }
        public long EndDateEpoch { get; set; }

        public string UserId { get; set; }
    }

    public class CreateUserTournamentCommandHandler : IRequestHandler<CreateUserTournamentCommand, Repository.Entity.UserTournament>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Repository.Entity.UserTournament> Handle(CreateUserTournamentCommand request, CancellationToken cancellationToken)
        {

            var tournamentRepository = _unitOfWork.GetRepository<Tournament, string>();
            var userTournamentRepository = _unitOfWork.GetRepository<Repository.Entity.UserTournament, string>();
            var userRepository = _unitOfWork.GetRepository<Repository.Entity.User, string>();


            var tournament = (await tournamentRepository.FindAsync(f => f.TournamentName == request.TournamentName && f.Active == 1)).FirstOrDefault();

            if (tournament != null)
            {
                throw new ApiException() { ErrorCode = "TOURNAMENT_NAME_MUST_BE_UNIQUE", ErrorMessage = "Tournament Name Must Be Unique", StatusCode = 500 };
            }

            var user = await userRepository.GetAsync(request.UserId);

            if (user == null)
            {
                throw new ApiException() { ErrorCode = "USER_NOT_FOUND", ErrorMessage = "Specified User Not Found", StatusCode = 500 };

            }

            tournament = new Tournament()
            {
                Active = 1,
                IsPrivate = true,
                Owner = user,
                TournamentName = request.TournamentName,
                Id = Guid.NewGuid().ToString("D"),
                TournameStartDateEpoch = request.StartDateEpoch,
                TournamentEndDateEpoch = request.EndDateEpoch,
                TournamentPassword = request.TournamentPassword
            };

            await tournamentRepository.AddAsync(tournament);

            var userTournament = new Repository.Entity.UserTournament()
            {
                Active = 1,
                Id = Guid.NewGuid().ToString("D"),
                Tournament = tournament,
                User = user
            };

            await userTournamentRepository.AddAsync(userTournament);
            await _unitOfWork.SaveChangesAsync();
            return userTournament;
        }
    }
}
