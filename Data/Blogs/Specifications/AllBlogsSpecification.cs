namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Shared.Patterns.Specification.Base;

    public class AllBlogsSpecification : ExpressionSpecification<Blog>
    {
        public override Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable) blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public override Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => blogs => blogs.OrderBy(blog => blog.Title);
    }
}