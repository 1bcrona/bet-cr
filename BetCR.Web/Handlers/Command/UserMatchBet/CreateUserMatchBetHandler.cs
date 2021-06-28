using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetCR.Web.Handlers.Command.UserMatchBet
{
    public class CreateUserMatchBetHandler : IRequestHandler<CreateUserMatchBetCommand, Match>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public CreateUserMatchBetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Match> Handle(CreateUserMatchBetCommand request, CancellationToken cancellationToken)
        {

            await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync(cancellationToken);

            var userMatchBetRepository = _unitOfWork.GetRepository<Repository.Entity.UserMatchBet, string>();

            var isBetExist = await userMatchBetRepository.FindAsync(w =>
                w.User.Id == request.UserId && w.Match.Id == request.MatchId && w.Active == 1);

            if (isBetExist.Any())
            {
                throw new ApiException() { ErrorCode = "USER_ALREADY_BET", StatusCode = 500, ErrorMessage = "User has already bet for this match" };
            }

            var userRepository = _unitOfWork.GetRepository<User, string>();
            var matchRepository = _unitOfWork.GetRepository<Match, string>();
            var user = await userRepository.GetAsync(request.UserId);
            var match = await matchRepository.GetAsync(request.MatchId);

            await userMatchBetRepository.AddAsync(new Repository.Entity.UserMatchBet
            {
                Id = Guid.NewGuid().ToString("D"),
                User = user,
                Match = match,
                AwayTeamScore = request.AwayTeamScore,
                UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Active = 1,
                HomeTeamScore = request.HomeTeamScore,
                Leverage = request.Leverage,
                ProcessState = 1,
            });

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);
            var updatedMatch = await matchRepository.GetAsync(request.MatchId);

            return updatedMatch;
        }

        #endregion Public Methods
    }
}