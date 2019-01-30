namespace Data.Users.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;

    public class UserByCredentialsSpecification : ExpressionSpecification<User>, IQueryableSpecification<User>
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

        public Expression<Func<User, bool>> Predicate => ToExpression();

        public Func<IQueryable<User>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public Func<IQueryable<User>, IOrderedQueryable<User>> OrderBy => null;
    }
}