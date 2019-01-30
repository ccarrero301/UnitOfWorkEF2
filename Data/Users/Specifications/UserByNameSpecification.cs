﻿namespace Data.Users.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;
    
    public class UserByNameSpecification : ExpressionSpecification<User>, IQueryableSpecification<User>
    {
        private readonly string _userName;

        public UserByNameSpecification(string userName) => _userName = userName;

        public override Expression<Func<User, bool>> ToExpression() => user => user.Username == _userName;

        public Expression<Func<User, bool>> Predicate => ToExpression();
           
        public Func<IQueryable<User>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public Func<IQueryable<User>, IOrderedQueryable<User>> OrderBy => null;
    }
}