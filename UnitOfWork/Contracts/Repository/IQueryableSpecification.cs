namespace UnitOfWork.Contracts.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;

    public interface IQueryableSpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; }

        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include { get; }

        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}