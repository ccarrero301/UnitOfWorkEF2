namespace Shared.Patterns.Specification.Utilities
{
    using System;
    using System.Linq.Expressions;

    internal static class ExpressionUtilities
    {
        public static Expression<Func<TEntity, bool>> GetFinalExpression<TEntity>(BinaryExpression expressionBody)
        {
            var paramExpr = Expression.Parameter(typeof(TEntity));

            expressionBody = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(expressionBody);

            return Expression.Lambda<Func<TEntity, bool>>(expressionBody, paramExpr);
        }
    }
}