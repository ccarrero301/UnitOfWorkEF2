namespace Services.Users
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using UnitOfWork.Contracts.Repository;
    using DataModel.Models;

    public class UserByCredentialsSpecification : IQueryableSpecification<User>
    {
        private readonly string _userName;
        private readonly string _password;

        public UserByCredentialsSpecification(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public Expression<Func<User, bool>> Predicate =>
            user => user.Username == _userName && user.Password == _password;

        public Func<IQueryable<User>, IIncludableQueryable<User, object>> Include => null;

        public Func<IQueryable<User>, IOrderedQueryable<User>> OrderBy => null;
    }
}