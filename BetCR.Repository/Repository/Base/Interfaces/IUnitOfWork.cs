using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BetCR.Repository.Repository.Base.Interfaces
{
    public interface IUnitOfWork
    {
        #region Public Properties

        public DbContext DbContext { get; }
        public string Id { get; }

        #endregion Public Properties

        #region Public Methods

        IRepository<T, TKey> GetRepository<T, TKey>() where T : EntityBase<TKey>;


        Task SaveChangesAsync();

        #endregion Public Methods
    }
}