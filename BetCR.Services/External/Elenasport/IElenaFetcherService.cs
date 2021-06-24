using System.Threading.Tasks;

namespace BetCR.Services.External.Elenasport
{
    public interface IElenaFetcherService
    {
        #region Public Methods

        Task GetFixtureResultsAsync();

        Task GetFixturesAsync();

        Task GetStandingsAsync();

        #endregion Public Methods
    }
}