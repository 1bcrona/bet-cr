using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Services.Base;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Services
{
    public class UserService : IUserService
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Properties

        public IUnitOfWork UnitOfWork => _unitOfWork;

        #endregion Public Properties

        #region Public Methods

        public async Task<User> GetById(string userId)
        {
            var repository = _unitOfWork.GetRepository<User, string>();
            return await repository.GetAsync(userId);
        }

        public async Task<User> GetUser(string email)
        {
            var repository = _unitOfWork.GetRepository<User, string>();
            var users = await repository.FindAsync(f => f.Email == email);
            return users?.FirstOrDefault();
        }

        public async Task<User> SaveUser(User user)
        {
            var repository = _unitOfWork.GetRepository<User, string>();
            var currentUser = await repository.GetAsync(user.Id);

            if (currentUser == null)
            {
                await repository.AddAsync(user);
            }
            else
            {
                await repository.UpdateAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        #endregion Public Methods
    }
}