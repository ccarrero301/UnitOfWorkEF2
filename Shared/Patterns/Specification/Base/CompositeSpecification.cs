namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq;
    using Contracts;
    using Implementations;

    public abstract class CompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        public abstract bool IsSatisfiedBy(TEntity entityToTest);

        public virtual Func<IQueryable<TEntity>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy => null;

        public ISpecification<TEntity> And(ISpecification<TEntity> specification) =>
            new AndSpecification<TEntity>(this, specification);

        public static CompositeSpecification<TEntity> operator &(CompositeSpecification<TEntity> specificationLeft,
            CompositeSpecification<TEntity> specificationRight) =>
            new AndSpecification<TEntity>(specificationLeft, specificationRight);

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification) =>
            new OrSpecification<TEntity>(this, specification);

        public static CompositeSpecification<TEntity> operator |(CompositeSpecification<TEntity> specificationLeft,
            CompositeSpecification<TEntity> specificationRight) =>
            new OrSpecification<TEntity>(specificationLeft, specificationRight);

        public ISpecification<TEntity> Not() => new NotSpecification<TEntity>(this);

        public static CompositeSpecification<TEntity> operator !(CompositeSpecification<TEntity> specification) =>
            new NotSpecification<TEntity>(specification);

        public ISpecification<TEntity> All() => new AllSpecification<TEntity>();
    }
}