namespace Shared.Patterns.Specification.Utilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Exceptions;
    using Base;

    internal static class ExpressionUtilities
    {
        public static Expression<Func<TEntity, bool>> GetFinalExpression<TEntity>(BinaryExpression expressionBody)
        {
            var paramExpr = Expression.Parameter(typeof(TEntity));

            expressionBody = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(expressionBody);

            return Expression.Lambda<Func<TEntity, bool>>(expressionBody, paramExpr);
        }

        public static Expression<Func<TEntity, bool>> GetFinalExpression<TEntity>(UnaryExpression expressionBody)
        {
            var paramExpr = Expression.Parameter(typeof(TEntity));

            expressionBody = (UnaryExpression) new ParameterReplacer(paramExpr).Visit(expressionBody);

            return Expression.Lambda<Func<TEntity, bool>>(expressionBody, paramExpr);
        }

        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderByExpression<TEntity>(
            QueryableExpressionSpecification<TEntity> leftExpressionSpecification,
            QueryableExpressionSpecification<TEntity> rightExpressionSpecification)
        {
            var isLeftExpressionSpecificationOrderByDefined = leftExpressionSpecification.OrderBy != null;
            var isRightExpressionSpecificationOrderByDefined = rightExpressionSpecification.OrderBy != null;

            if (isLeftExpressionSpecificationOrderByDefined && isRightExpressionSpecificationOrderByDefined)
                throw new SpecificationOrderByException(
                    "Cannot have two different order by for the joint specification");

            if (isLeftExpressionSpecificationOrderByDefined)
                return leftExpressionSpecification.OrderBy;

            return isRightExpressionSpecificationOrderByDefined ? rightExpressionSpecification.OrderBy : null;
        }

        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderByExpression<TEntity>(
            QueryableExpressionSpecification<TEntity> expressionSpecification) => expressionSpecification.OrderBy;
    }
}