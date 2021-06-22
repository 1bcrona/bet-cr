using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;

namespace BetCR.Services
{
    public interface IUserMatchBetService
    {
        public Task CalculateUserPointsAsync();
        public Task CalculateUserPointsAsync(string id);
    }
    public class UserMatchBetService : IUserMatchBetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserMatchBetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

                CalculateUserPoints(bet);
                await repository.UpdateAsync(bet);
                await _unitOfWork.SaveChangesAsync();
            }


        }

        public async Task CalculateUserPointsAsync(string id)
        {

            var repository = _unitOfWork.GetRepository<UserMatchBet, string>();

            var bet = await repository.GetAsync(id);

            if (bet != null && bet.Active == 1)
            {
                CalculateUserPoints(bet);
                await repository.UpdateAsync(bet);
                await _unitOfWork.SaveChangesAsync();
            }

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

        private int CalculatePointForBet(int matchHomeScore, int matchAwayScore, int userBetHomeScore, int userBetAwayScore)
        {
            var point = -1;
            var matchDiff = matchHomeScore - matchAwayScore;
            var userDiff = userBetHomeScore - userBetAwayScore;

            if ((matchDiff * userDiff <= 0) && (matchDiff != 0 || userDiff != 0)) return point;
            point = 1;
            if (userDiff != matchDiff) return point;
            point = 2;
            if (userBetHomeScore == matchHomeScore)
            {
                point = 5;
            }

            return point;

        }

    }
}
