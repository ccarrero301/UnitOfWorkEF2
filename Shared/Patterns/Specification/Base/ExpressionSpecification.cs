namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public virtual Expression<Func<TEntity, bool>> ToExpression() => null;

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            var predicate = ToExpression().Compile();
            return predicate(entityToTest);
        }
    }
}