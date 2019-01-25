namespace DataModel.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Patterns.Specification.Base;
    using Models;

    public class UserByPasswordSpecification : ExpressionSpecification<User>
    {
        private readonly string _userPassword;

        public UserByPasswordSpecification(string userPassword) =>
            _userPassword = UserUtils.EncryptPassword(userPassword);

        public override Expression<Func<User, bool>> ToExpression() => user => user.Password == _userPassword;
    }
}