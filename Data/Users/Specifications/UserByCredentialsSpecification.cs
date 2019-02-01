namespace Data.Users.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;

    public class UserByCredentialsSpecification : QueryableExpressionSpecification<User>
    {
        private readonly string _userName;
        private readonly string _password;

        public UserByCredentialsSpecification(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public override Expression<Func<User, bool>> ToExpression() =>
            user => user.Username == _userName && user.Password == _password;
    }
}