namespace UnitOfWork.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Contracts.PagedList;
    using Contracts.Repository;

    internal class QueryableRepository<TEntity> : IQueryableRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public QueryableRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true)
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy?.Invoke(query).ToPagedList(pageIndex, pageSize) ?? query.ToPagedList(pageIndex, pageSize);
        }

        public Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy?.Invoke(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken) ??
                   query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true)
            where TResult : class
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy?.Invoke(query).Select(selector).ToPagedList(pageIndex, pageSize) ??
                   query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
            where TResult : class
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy?.Invoke(query).Select(selector)
                       .ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken) ?? query.Select(selector)
                       .ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy?.Invoke(query).FirstOrDefaultAsync() ?? query.FirstOrDefaultAsync();
        }

        public Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            var query = GetQuery(disableTracking, include, predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).FirstOrDefaultAsync()
                : query.Select(selector).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => DbSet.FromSql(sql, parameters);

        public TEntity Find(params object[] keyValues) => DbSet.Find(keyValues);

        public Task<TEntity> FindAsync(params object[] keyValues) => DbSet.FindAsync(keyValues);

        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) =>
            DbSet.FindAsync(keyValues, cancellationToken);

        public int Count(Expression<Func<TEntity, bool>> predicate = null) =>
            predicate == null ? DbSet.Count() : DbSet.Count(predicate);

        private IQueryable<TEntity> GetQuery(bool disableTracking,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }
    }
}