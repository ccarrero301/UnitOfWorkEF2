namespace Data.Users.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;

    public class UserByNameSpecification : QueryableExpressionSpecification<User>
    {
        private readonly string _userName;

        public UserByNameSpecification(string userName) => _userName = userName;

        public override Expression<Func<User, bool>> ToExpression() => user => user.Username == _userName;
    }
}