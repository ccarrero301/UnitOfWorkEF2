﻿namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Shared.Patterns.Specification.Base;

    public class BlogWithPostsSpecification : QueryableExpressionSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogWithPostsSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;

        public override Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable) blogs.Include(blog => blog.Posts);
    }
}