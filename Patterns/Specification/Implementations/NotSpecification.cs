namespace Patterns.Specification.Implementations
{
    using System;
    using System.Linq;
    using Base;
    using Contracts;

    internal sealed class NotSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _specification;

        public NotSpecification(ISpecification<TEntity> specification) => _specification = specification;

        public override bool IsSatisfiedBy(TEntity entityToTest) => !_specification.IsSatisfiedBy(entityToTest);

        public override Func<IQueryable<TEntity>, TResult> ToInclude<TResult>() => _specification.ToInclude<TResult>();
    }
}