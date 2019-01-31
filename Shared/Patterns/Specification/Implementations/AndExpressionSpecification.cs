namespace Shared.Patterns.Specification.Implementations
{
    using System;
    using System.Linq.Expressions;
    using Utilities;
    using Base;

    public class AndExpressionSpecification<TEntity> : ExpressionSpecification<TEntity>
    {
        private readonly ExpressionSpecification<TEntity> _leftExpressionSpecification;
        private readonly ExpressionSpecification<TEntity> _rightExpressionSpecification;

        public AndExpressionSpecification(ExpressionSpecification<TEntity> leftExpressionSpecification,
            ExpressionSpecification<TEntity> rightExpressionSpecification)
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