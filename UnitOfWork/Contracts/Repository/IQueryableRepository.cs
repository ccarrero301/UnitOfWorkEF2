﻿namespace UnitOfWork.Contracts.Repository
{
    using System;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shared.Patterns.Specification.Base;
    using PagedList;

    public interface IQueryableRepository<TEntity> where TEntity : class
    {
        IPagedList<TEntity> GetPagedList(QueryableExpressionSpecification<TEntity> specification = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true);

        Task<IPagedList<TEntity>> GetPagedListAsync(QueryableExpressionSpecification<TEntity> specification = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken));

        IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
            QueryableExpressionSpecification<TEntity> specification = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true) where TResult : class;

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            QueryableExpressionSpecification<TEntity> specification = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

        Task<TEntity> GetFirstOrDefaultAsync(QueryableExpressionSpecification<TEntity> specification = null,
            bool disableTracking = true);

        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            QueryableExpressionSpecification<TEntity> specification = null,
            bool disableTracking = true);

        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        int Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}