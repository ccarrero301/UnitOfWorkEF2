namespace Shared.Patterns.Specification.Implementations
{
    using System;
    using System.Linq.Expressions;
    using Utilities;
    using Base;

    public class OrExpressionSpecification<TEntity> : ExpressionSpecification<TEntity>
    {
        private readonly ExpressionSpecification<TEntity> _leftExpressionSpecification;
        private readonly ExpressionSpecification<TEntity> _rightExpressionSpecification;

        public OrExpressionSpecification(ExpressionSpecification<TEntity> leftExpressionSpecification,
            ExpressionSpecification<TEntity> rightExpressionSpecification)
        {
            _rightExpressionSpecification = rightExpressionSpecification;
            _leftExpressionSpecification = leftExpressionSpecification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpressionSpecification = _leftExpressionSpecification.ToExpression();
            var rightExpressionSpecification = _rightExpressionSpecification.ToExpression();

            var expressionBody = Expression.OrElse(leftExpressionSpecification.Body, rightExpressionSpecification.Body);

            return ExpressionUtilities.GetFinalExpression<TEntity>(expressionBody);
        }
    }
}