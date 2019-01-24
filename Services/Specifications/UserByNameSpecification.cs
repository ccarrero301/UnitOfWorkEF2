namespace Services.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Patterns.Specification.Base;
    using DataModel.Models;

    public class UserByNameSpecification : ExpressionSpecification<User>
    {
        private readonly string _userName;

        public UserByNameSpecification(string _userName)
        {
            this._userName = _userName;
        }

        public override Expression<Func<User, bool>> ToExpression() => user => user.Username == _userName;
    }
}