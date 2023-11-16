using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.ValueObject;
using BetCR.Web.Models.Response;

namespace BetCR.Web.Handlers.Query.Tournament
{
    public class GetTournamentStandingsQuery : IRequest<List<TournamentStandingsModel>>
    {
        #region Public Properties

        public string TournamentId { get; set; }

        #endregion Public Properties
    }

    public class GetTournamentStandingsQueryHandler : IRequestHandler<GetTournamentStandingsQuery, List<TournamentStandingsModel>>
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public GetTournamentStandingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<TournamentStandingsModel>> Handle(GetTournamentStandingsQuery request, CancellationToken cancellationToken)
        {
            var betRepository = _unitOfWork.GetRepository<UserMatchBet, string>();
            var tournamentRepository = _unitOfWork.GetRepository<Repository.Entity.Tournament, string>();
            var userRepository = _unitOfWork.GetRepository<User, string>();
            var tournament = await tournamentRepository.GetAsync(request.TournamentId);

            var bets = await betRepository.FindAsync(f =>
                f.Active == 1 && f.User.Active == 1 && f.Match.MatchDateEpoch > tournament.TournameStartDateEpoch &&
                f.User.UserTournameRels.Any(w => w.Tournament.Id == request.TournamentId && w.Active == 1) &&
                f.ProcessState == 2);

            var groupedBets = bets.GroupBy(g => g.User.Id);

            var userBetPoints = groupedBets.Select(userBet => new TournamentStandingsModel()
                {
                    User = userBet.First().User,
                    WinCount = userBet.Count(w => w.UserBetPoint > 0),
                    LossCount = userBet.Count(w => w.UserBetPoint < 0),
                    WinMatchCount = userBet.Where(w => w.UserBetPointDefault == UserBetPoint.WinMatch).Sum(w => w.UserBetPoint).Value,
                    WinDifferenceCount = userBet.Where(w => w.UserBetPointDefault == UserBetPoint.WinDifference).Sum(w => w.UserBetPoint).Value,
                    WinMatchScoreCount = userBet.Where(w => w.UserBetPointDefault == UserBetPoint.WinMatchScore).Sum(w => w.UserBetPoint).Value,
                    LossMatchCount = userBet.Where(w => w.UserBetPointDefault == UserBetPoint.LossMatch).Sum(w => w.UserBetPoint).Value,
                    TotalPoints = userBet.Sum(s => s.UserBetPoint ?? 0)
                })
                .ToList();

            var tournamentUsers = await userRepository.FindAsync(f =>
                f.Active == 1 && f.UserTournameRels.Any(w => w.Tournament.Id == request.TournamentId && w.Active == 1));


            var nonBetUsers = tournamentUsers.Where(a => userBetPoints.All(a1 => a1.User.Id != a.Id));


            userBetPoints.AddRange(nonBetUsers.Select(user => new TournamentStandingsModel()
            {
                User = user
            }));

            userBetPoints = userBetPoints
                .OrderByDescending(o => o.TotalPoints)
                .ThenByDescending(o => o.WinCount)
                .ThenByDescending(o => o.WinMatchScoreCount)
                .ThenBy(o => o.User.FullName).ToList();

            return userBetPoints;
        }

        #endregion Public Methods
    }
}