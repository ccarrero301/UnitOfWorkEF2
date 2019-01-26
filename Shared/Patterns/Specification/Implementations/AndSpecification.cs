namespace Shared.Patterns.Specification.Implementations
{
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
    }
}