namespace Services.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Patterns.Specification.Base;
    using DataModel.Models;

    public class UserByPasswordSpecification : ExpressionSpecification<User>
    {
        private readonly string _userPassword;

        public UserByPasswordSpecification(string _userPassword)
        {
            this._userPassword = UserUtils.EncryptPassword(_userPassword);
        }

        public override Expression<Func<User, bool>> ToExpression() => user => user.Password == _userPassword;
    }
}