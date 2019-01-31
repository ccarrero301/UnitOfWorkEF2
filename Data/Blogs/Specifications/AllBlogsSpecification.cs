﻿namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;

    public class AllBlogsSpecification : QueryableExpressionSpecification<Blog>
    {
        public override Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable) blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => blogs => blogs.OrderBy(blog => blog.Title);
    }
}