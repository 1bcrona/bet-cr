using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;

namespace BetCR.Web.Handlers.Command
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

            var userMatchBetRepository = _unitOfWork.GetRepository<UserMatchBet, string>();
            var userRepository = _unitOfWork.GetRepository<User, string>();
            var matchRepository = _unitOfWork.GetRepository<Match, string>();

            var user = await userRepository.GetAsync(request.UserId);
            var match = await matchRepository.GetAsync(request.MatchId);

            await userMatchBetRepository.AddAsync(new UserMatchBet()
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
