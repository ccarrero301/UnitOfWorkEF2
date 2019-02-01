namespace Shared.Patterns.Specification.Implementations
{
    using System;
    using System.Linq.Expressions;
    using Utilities;
    using Base;

    public class AndExpressionSpecification<TEntity> : QueryableExpressionSpecification<TEntity>
    {
        private readonly QueryableExpressionSpecification<TEntity> _leftExpressionSpecification;
        private readonly QueryableExpressionSpecification<TEntity> _rightExpressionSpecification;

        public AndExpressionSpecification(QueryableExpressionSpecification<TEntity> leftExpressionSpecification,
            QueryableExpressionSpecification<TEntity> rightExpressionSpecification)
        {
            _rightExpressionSpecification = rightExpressionSpecification;
            _leftExpressionSpecification = leftExpressionSpecification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpressionSpecification = _leftExpressionSpecification.ToExpression();
            var rightExpressionSpecification = _rightExpressionSpecification.ToExpression();

            var expressionBody =
                Expression.AndAlso(leftExpressionSpecification.Body, rightExpressionSpecification.Body);

            var orderBy = ExpressionUtilities.GetOrderByExpression(_leftExpressionSpecification, _rightExpressionSpecification);

            SetOrderBy(orderBy);

            return ExpressionUtilities.GetFinalExpression<TEntity>(expressionBody);
        }
    }
}