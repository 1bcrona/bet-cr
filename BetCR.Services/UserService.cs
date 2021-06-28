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
            _unitOfWork.EnableTracking();
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
            await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
            var repository = _unitOfWork.GetRepository<User, string>();
            var currentUser = await repository.GetAsync(user.Id);

            if (currentUser == null)
            {
                await repository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                await repository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }

            await transaction.CommitAsync();


            return user;
        }

        #endregion Public Methods
    }
}