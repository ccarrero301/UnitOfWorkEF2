namespace Shared.Patterns.Specification.Implementations
{
    using System;
    using System.Linq.Expressions;
    using Utilities;
    using Base;

    internal sealed class NotExpressionSpecification<TEntity> : QueryableExpressionSpecification<TEntity>
    {
        private readonly QueryableExpressionSpecification<TEntity> _expressionSpecification;

        public NotExpressionSpecification(QueryableExpressionSpecification<TEntity> expressionSpecification) =>
            _expressionSpecification = expressionSpecification;

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expressionSpecification = _expressionSpecification.ToExpression();

            var expressionBody = Expression.Not(expressionSpecification.Body);

            var orderBy = ExpressionUtilities.GetOrderByExpression(_expressionSpecification);

            SetOrderBy(orderBy);

            return ExpressionUtilities.GetFinalExpression<TEntity>(expressionBody);
        }
    }
}