using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;

namespace BetCR.Web.Handlers.Command.UserMatchBet
{
    public class CreateUserMatchBetHandler : IRequestHandler<CreateUserMatchBetCommand, Match>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserMatchBetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Match> Handle(CreateUserMatchBetCommand request, CancellationToken cancellationToken)
        {

            var userMatchBetRepository = _unitOfWork.GetRepository<Repository.Entity.UserMatchBet, string>();

            var isBetExist = await userMatchBetRepository.FindAsync(w =>
                w.User.Id == request.UserId && w.Match.Id == request.MatchId && w.Active == 1);

            if (isBetExist.Any())
            {
                throw new Exception("USER_ALREADY_BET");
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

            var updatedMatch = await matchRepository.GetAsync(request.MatchId);

            return updatedMatch;

        }
    }


}
