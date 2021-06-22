using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetCR.Repository.Entity.Base;
using BetCR.Repository.Repository.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BetCR.Repository.Repository.Base
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T : EntityBase<TKey>
    {
        #region Public Fields

        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        #endregion Public Fields

        #region Public Constructors

        public BaseRepository(DbContext context)
        {
            this._dbContext = context;
            this._dbSet = context.Set<T>();
        }

        public SQLiteDbContext DbContext

        {
            get
            {
                return _dbContext as SQLiteDbContext;
            }
        }

        #endregion Public Constructors

        #region Public Methods

        #region Async

        public async Task<T> AddAsync(T entity)
        {
            entity.UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TKey id)
        {
            var existing = (await FindAsync(f => Equals(f.Id, id))).FirstOrDefault();

            if (existing != null)
            {
                await DeleteAsync(existing);
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var items = await _dbSet.Where(predicate).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(null, null);
        }

        public async Task<IEnumerable<T>> GetAllAsync(int? pageIndex, int? pageSize)
        {
            List<T> items;
            if (pageSize != null && pageIndex != null)
            {
                items = await _dbSet.Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                items = await _dbSet.ToListAsync();
            }

            return items;
        }

        public async Task<T> GetAsync(TKey id)
        {
            var item = await _dbSet.FindAsync(id);
            return item;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpsertDateEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public async Task<T> UpsertAsync(T entity)
        {
            var existing = await GetAsync(entity.Id);

            if (existing == null)
            {
                await AddAsync(entity);
            }
            else
            {
                await UpdateAsync(entity);
            }

            return entity;
        }

        #endregion Async

        #endregion Public Methods
    }
}