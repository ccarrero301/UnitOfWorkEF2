namespace DataModel.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Patterns.Specification.Base;
    using Models;

    public class UserByUserNameSpecification : ExpressionSpecification<User>
    {
        private readonly string _userName;

        public UserByUserNameSpecification(string userName) => this._userName = userName;

        public override Expression<Func<User, bool>> ToExpression() => user => user.Username == _userName;
    }
}