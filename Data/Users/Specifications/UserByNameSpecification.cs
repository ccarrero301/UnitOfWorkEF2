namespace Data.Users.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts.Repository;

    public class UserByNameSpecification : IQueryableSpecification<User>
    {
        private readonly string _userName;

        public UserByNameSpecification(string userName)
        {
            _userName = userName;
        }

        public Expression<Func<User, bool>> Predicate =>
            user => user.Username == _userName;

        public Func<IQueryable<User>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public Func<IQueryable<User>, IOrderedQueryable<User>> OrderBy => null;
    }
}