namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            var predicate = ToExpression().Compile();
            return predicate(entityToTest);
        }
    }
}