namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq;
    using Contracts;

    public abstract class QueryableSpecification<TEntity> : IQueryableSpecification<TEntity>
    {
        public virtual Func<IQueryable<TEntity>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; private set; }

        public virtual void SetOrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) =>
            OrderBy = orderBy;
    }
}