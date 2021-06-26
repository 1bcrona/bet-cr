using System.Threading.Tasks;

namespace BetCR.Services
{
    public interface IUserMatchBetService
    {
        public Task CalculateUserPointsAsync();
        public Task CalculateUserPointsAsync(string id);
    }
}