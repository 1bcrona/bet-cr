using System.Threading.Tasks;

namespace BetCR.Services
{
    public interface IUserMatchBetService
    {
        #region Public Methods

        public Task CalculateUserPointsAsync();

        public Task CalculateUserPointsAsync(string id);

        #endregion Public Methods
    }
}