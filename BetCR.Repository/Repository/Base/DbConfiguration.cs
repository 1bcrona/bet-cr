using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BetCR.Repository.Repository.Base
{
    public class DataContext : DbContext
    {
        #region Private Fields

        private static IConfigurationRoot _configuration;

        private string _connectionString;

        #endregion Private Fields

        #region Public Constructors

        public DataContext() : this(Configuration.GetConnectionString("DefaultConnection"))
        {
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion Public Constructors

        #region Public Properties

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    _configuration = builder.Build();
                }
                return _configuration;
            }
        }

        public string ConnectionString { get { return _connectionString; } }

        #endregion Public Properties
    }
}