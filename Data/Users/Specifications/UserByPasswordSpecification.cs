namespace Data.Users.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;

    public class UserByPasswordSpecification : QueryableExpressionSpecification<User>
    {
        private readonly string _password;

        public UserByPasswordSpecification(string password) => _password = password;

        public override Expression<Func<User, bool>> ToExpression() => user => user.Password == _password;
    }
}