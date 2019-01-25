namespace Patterns.Specification.Implementations
{
    using Base;
    using Contracts;

    internal sealed class NotSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _specification;

        public NotSpecification(ISpecification<TEntity> specification) => _specification = specification;

        public override bool IsSatisfiedBy(TEntity entityToTest) => !_specification.IsSatisfiedBy(entityToTest);
    }
}