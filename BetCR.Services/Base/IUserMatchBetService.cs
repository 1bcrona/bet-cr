using System.Threading.Tasks;

namespace BetCR.Services.Base
{
    public interface IUserMatchBetService
    {
        #region Public Methods

        public Task CalculateUserPointsAsync();

        #endregion Public Methods
    }
}