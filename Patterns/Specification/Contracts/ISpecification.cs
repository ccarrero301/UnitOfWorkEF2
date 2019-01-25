namespace Patterns.Specification.Contracts
{
    using System;
    using System.Linq;

    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entityToTest);

        Func<IQueryable<TEntity>, TResult> ToInclude<TResult>();

        ISpecification<TEntity> And(ISpecification<TEntity> specification);

        ISpecification<TEntity> Or(ISpecification<TEntity> specification);

        ISpecification<TEntity> Not();

        ISpecification<TEntity> All();
    }
}