namespace Shared.Patterns.Specification.Utilities
{
    using System.Linq.Expressions;

    internal sealed class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        internal ParameterReplacer(ParameterExpression parameter) => _parameter = parameter;

        protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(_parameter);
    }
}