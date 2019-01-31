namespace Shared.Patterns.Specification.Contracts
{
    using System;
    using System.Linq;

    public interface IQueryableSpecification<TEntity>
    {
        Func<IQueryable<TEntity>, TIncludableQueryable> Include<TIncludableQueryable>();

        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}