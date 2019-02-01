namespace Shared.Patterns.Specification.Implementations
{
    using Base;

    internal sealed class AllSpecification<TEntity> : ExpressionSpecification<TEntity>
    {
        public override bool IsSatisfiedBy(TEntity entityToTest) => true;
    }
}