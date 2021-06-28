using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Repository.ValueObject;
using System;
using System.Threading.Tasks;

namespace BetCR.Services
{
    public class UserMatchBetService : IUserMatchBetService
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public UserMatchBetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.EnableTracking();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CalculateUserPointsAsync()
        {
            var repository = _unitOfWork.GetRepository<UserMatchBet, string>();

            var bets = await repository.FindAsync(f =>
                                                        f.ProcessState == 1
                                                       && f.Match.ResultState == 2
                                                       && f.Active == 1
                                                       && f.Match.Active == 1);

            foreach (var bet in bets)
            {
                await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
                CalculateUserPoints(bet);
                await repository.UpdateAsync(bet);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

          #endregion Public Methods

        #region Private Methods

        private int CalculatePointForBet(int matchHomeScore, int matchAwayScore, int userBetHomeScore, int userBetAwayScore)
        {
            var point = UserBetPoint.LossMatch;
            var matchDiff = matchHomeScore - matchAwayScore;
            var userDiff = userBetHomeScore - userBetAwayScore;

            if ((matchDiff * userDiff <= 0) && (matchDiff != 0 || userDiff != 0)) return point;
            point = UserBetPoint.WinMatch;
            if (userDiff != matchDiff) return point;
            point = UserBetPoint.WinDifference;
            if (userBetHomeScore == matchHomeScore)
            {
                point = UserBetPoint.WinMatchScore;
            }

            return point;
        }

        private void CalculateUserPoints(UserMatchBet userMatchBet)
        {
            var matchHomeScore = userMatchBet.Match.MatchEvent.HomeTeamScore;
            var matchAwayScore = userMatchBet.Match.MatchEvent.AwayTeamScore;
            if (matchAwayScore == null || matchHomeScore == null) return;
            if (matchAwayScore.Value < 0 || matchHomeScore < 0) return;

            var userBetHomeScore = userMatchBet.HomeTeamScore;
            var userBetAwayScore = userMatchBet.AwayTeamScore;
            var leverage = Math.Max(userMatchBet.Leverage, 1);

            userMatchBet.UserBetPointDefault = CalculatePointForBet(matchHomeScore.Value, matchAwayScore.Value, userBetHomeScore, userBetAwayScore);
            userMatchBet.UserBetPoint = userMatchBet.UserBetPointDefault * leverage;
            userMatchBet.ProcessState = 2;
        }

        #endregion Private Methods
    }
}