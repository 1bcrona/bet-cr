using BetCR.Repository.Entity;
using BetCR.Services.Interface;
using System.Threading.Tasks;

namespace BetCR.Services.Base
{
    public interface IUserService : IService
    {
        #region Public Methods

        public Task<User> GetById(string userId);

        public Task<User> GetUser(string email);

        public Task<User> SaveUser(User user);

        #endregion Public Methods
    }
}