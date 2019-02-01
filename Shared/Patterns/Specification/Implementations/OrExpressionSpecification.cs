namespace Shared.Patterns.Specification.Implementations
{
    using System;
    using System.Linq.Expressions;
    using Utilities;
    using Base;

    internal sealed class OrExpressionSpecification<TEntity> : QueryableExpressionSpecification<TEntity>
    {
        private readonly QueryableExpressionSpecification<TEntity> _leftExpressionSpecification;
        private readonly QueryableExpressionSpecification<TEntity> _rightExpressionSpecification;

        public OrExpressionSpecification(QueryableExpressionSpecification<TEntity> leftExpressionSpecification,
            QueryableExpressionSpecification<TEntity> rightExpressionSpecification)
        {
            _rightExpressionSpecification = rightExpressionSpecification;
            _leftExpressionSpecification = leftExpressionSpecification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpressionSpecification = _leftExpressionSpecification.ToExpression();
            var rightExpressionSpecification = _rightExpressionSpecification.ToExpression();

            var expressionBody = Expression.OrElse(leftExpressionSpecification.Body, rightExpressionSpecification.Body);

            var orderBy =
                ExpressionUtilities.GetOrderByExpression(_leftExpressionSpecification, _rightExpressionSpecification);

            SetOrderBy(orderBy);

            return ExpressionUtilities.GetFinalExpression<TEntity>(expressionBody);
        }
    }
}