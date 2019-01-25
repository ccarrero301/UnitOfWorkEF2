namespace DataModel.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Patterns.Specification.Base;
    using Models;

    public class AllBlogsWithPostsSpecification : ExpressionSpecification<Blog>
    {
        public override Expression<Func<Blog, bool>> ToExpression() => blog => true;

        public override Func<IQueryable<Blog>, TResult> ToInclude<TResult>() =>
            blogs => (TResult) (blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments));
    }
}