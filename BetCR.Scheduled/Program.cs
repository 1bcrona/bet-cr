using System.Threading.Tasks;

namespace BetCR.Scheduled
{
    internal class Program
    {
        #region Private Methods

        private static readonly App _APP = new();

        private static async Task Main(string[] args)
        {
            await _APP.Run();
        }

        #endregion Private Methods
    }
}