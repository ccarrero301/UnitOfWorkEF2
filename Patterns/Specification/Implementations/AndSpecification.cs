namespace Patterns.Specification.Implementations
{
    using System;
    using System.Linq;
    using Base;
    using Contracts;

    internal sealed class AndSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _leftSpecification;
        private readonly ISpecification<TEntity> _rightSpecification;

        public AndSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override bool IsSatisfiedBy(TEntity entityToTest) =>
            _leftSpecification.IsSatisfiedBy(entityToTest) && _rightSpecification.IsSatisfiedBy(entityToTest);

        public override Func<IQueryable<TEntity>, TResult> ToInclude<TResult>()
        {
            var leftSpecificationFunction = _leftSpecification.ToInclude<TResult>();
            var rightSpecificationFunction = _rightSpecification.ToInclude<TResult>();

            if (leftSpecificationFunction != null && rightSpecificationFunction != null)
                throw new Exception("only one part of the composition can define the included properties");

            return leftSpecificationFunction ?? rightSpecificationFunction;
        }
    }
}