namespace Services.Users
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using UnitOfWork.Contracts.Repository;
    using DataModel.Models;

    public class UserByNameSpecification : IQueryableSpecification<User>
    {
        private readonly string _userName;

        public UserByNameSpecification(string userName)
        {
            _userName = userName;
        }

        public Expression<Func<User, bool>> Predicate =>
            user => user.Username == _userName;

        public Func<IQueryable<User>, IIncludableQueryable<User, object>> Include => null;

        public Func<IQueryable<User>, IOrderedQueryable<User>> OrderBy => null;
    }
}