namespace UnitOfWork.Contracts.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IQueryableSpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; }

        Func<IQueryable<TEntity>, TIncludableQueryable> Include<TIncludableQueryable>();

        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}