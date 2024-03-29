﻿using BetCR.Repository.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BetCR.Repository.Repository.Base.Interfaces
{
    public interface IRepository<T, in TKey> where T : BaseEntity<TKey>
    {
        #region Public Properties

        public SQLiteDbContext DbContext { get; }

        #endregion Public Properties

        #region Public Methods

        Task<T> AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(TKey id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(int? pageIndex, int? pageSize);

        Task<T> GetAsync(TKey id);

        Task SaveChangesAsync();

        Task<T> UpdateAsync(T entity);

        Task<T> UpsertAsync(T entity);

        #endregion Public Methods
    }
}