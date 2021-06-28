using BetCR.Repository.Entity.Base;
using BetCR.Repository.Repository.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BetCR.Repository.Repository.Base
{
    public class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        #region Public Fields

        public string _id;

        #endregion Public Fields

        #region Private Fields

        private readonly DbContext _dbContext;
        private ILogger _logger;
        private bool disposed = false;

        #endregion Private Fields

        #region Public Constructors

        public BaseUnitOfWork(DbContext context)
        {
            this._dbContext = context;

            this._id = Guid.NewGuid().ToString("D");
        }

        #endregion Public Constructors

        #region Public Properties

        public DbContext DbContext => _dbContext;

        public string Id => _id;

        public ILogger Logger => _logger;

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T, TKey> GetRepository<T, TKey>() where T : EntityBase<TKey>
        {
            return new BaseRepository<T, TKey>(_dbContext);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                {
                    this.disposed = true;
                }
            this.disposed = true;
        }

        #endregion Protected Methods
    }
}