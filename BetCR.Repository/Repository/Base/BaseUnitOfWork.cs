using BetCR.Repository.Entity.Base;
using BetCR.Repository.Repository.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using BetCR.Library.Tracking.Infrastructure;

namespace BetCR.Repository.Repository.Base
{
    public class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        #region Public Fields

        public string _id;

        #endregion Public Fields

        #region Private Fields

        private readonly DataContext _dbContext;
        private readonly IPublisher _publisher;
        private ILogger _logger;
        private bool disposed = false;

        #endregion Private Fields

        #region Public Constructors

        public BaseUnitOfWork(DataContext context, IPublisher publisher)
        {
            _dbContext = context;
            _publisher = publisher;

            _id = Guid.NewGuid().ToString("D");
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T, TKey> GetRepository<T, TKey>() where T : BaseEntity<TKey>
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
            if (!disposed)
                if (disposing)
                {
                    disposed = true;
                    DbContext.ChangeTracker.StateChanged -= ChangeTracker_StateChanged;
                }

            disposed = true;
        }

        public void EnableTracking()
        {
            DbContext.ChangeTracker.DetectChanges();
            DbContext.ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }


        private void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            if (e.Entry.Entity.GetType().GetCustomAttribute<NotTrackingAttribute>() != null) return;


            var context = (sender as Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker)?.Context;
            var entityId = e.Entry.Entity.GetType().GetProperty("Id")?.GetValue(e.Entry.Entity)?.ToString();
            var entityChangeModel = new EntityChangeModel {EntityId = entityId, Entity = e.Entry.Entity, Context = context};

            entityChangeModel.EventType = e.NewState switch
            {
                EntityState.Deleted => string.Join(".", e.Entry.Entity.GetType().Name.ToLowerInvariant(), "deleted"),
                EntityState.Unchanged when e.OldState == EntityState.Modified => string.Join(".", e.Entry.Entity.GetType().Name.ToLowerInvariant(), "updated"),
                EntityState.Unchanged when e.OldState == EntityState.Added => string.Join(".",
                    e.Entry.Entity.GetType().Name.ToLowerInvariant(), "added"),
                _ => entityChangeModel.EventType
            };
            if (entityChangeModel.EventType != null)
                _publisher?.Publish(entityChangeModel);
        }

        #endregion Protected Methods
    }
}