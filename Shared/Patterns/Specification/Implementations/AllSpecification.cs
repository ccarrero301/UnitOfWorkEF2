namespace Shared.Patterns.Specification.Implementations
{
    using Base;

    internal sealed class AllSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public override bool IsSatisfiedBy(TEntity entityToTest) => true;
    }
}