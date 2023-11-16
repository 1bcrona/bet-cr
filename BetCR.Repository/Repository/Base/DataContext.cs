using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BetCR.Repository.Repository.Base
{
    public class DataContext : DbContext
    {
        #region Private Fields

        private readonly IConfiguration _configuration;

        #endregion Private Fields



        #region Private Fields

        private string _connectionString;

        #endregion Private Fields

        #region Public Constructors

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion Public Constructors

        #region Public Properties

        public string ConnectionString { get { return _connectionString; } }


        

        #endregion Public Properties
    }
}