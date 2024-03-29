﻿using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Command.UserTournament
{
    public class CreateUserTournamentCommand : IRequest<Repository.Entity.UserTournament>
    {
        #region Public Properties

        public long EndDateEpoch { get; set; }
        public long StartDateEpoch { get; set; }
        public string TournamentName { get; set; }
        public string TournamentPassword { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }

    public class CreateUserTournamentCommandHandler : IRequestHandler<CreateUserTournamentCommand, Repository.Entity.UserTournament>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly BetCR.Library.Tracking.Infrastructure.IPublisher _publisher;

        #endregion Private Fields

        #region Public Constructors

        public CreateUserTournamentCommandHandler(IUnitOfWork unitOfWork, BetCR.Library.Tracking.Infrastructure.IPublisher publisher)
        {
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Repository.Entity.UserTournament> Handle(CreateUserTournamentCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync(cancellationToken);
            var tournamentRepository = _unitOfWork.GetRepository<Tournament, string>();
            var userTournamentRepository = _unitOfWork.GetRepository<Repository.Entity.UserTournament, string>();
            var userRepository = _unitOfWork.GetRepository<User, string>();

            var tournament =
                (await tournamentRepository.FindAsync(f =>
                    f.TournamentName == request.TournamentName && f.Active == 1)).FirstOrDefault();

            if (tournament != null)
                throw new ApiException()
                {
                    ErrorCode = "TOURNAMENT_NAME_MUST_BE_UNIQUE",
                    ErrorMessage = "Tournament Name Must Be Unique",
                    StatusCode = 500
                };

            var user = await userRepository.GetAsync(request.UserId);

            if (user == null)
                throw new ApiException()
                    {ErrorCode = "USER_NOT_FOUND", ErrorMessage = "Specified User Not Found", StatusCode = 500};

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
            await _unitOfWork.SaveChangesAsync();
            var userTournament = new Repository.Entity.UserTournament()
            {
                Active = 1,
                Id = Guid.NewGuid().ToString("D"),
                Tournament = tournament,
                User = user
            };
            await userTournamentRepository.AddAsync(userTournament);
            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync(cancellationToken);
            return userTournament;
        }

        #endregion Public Methods
    }
}