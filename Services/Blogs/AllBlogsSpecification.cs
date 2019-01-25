namespace Services.Blogs
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using UnitOfWork.Contracts.Repository;
    using DataModel.Models;

    public class AllBlogsSpecification : IQueryableSpecification<Blog>
    {
        public Expression<Func<Blog, bool>> Predicate => null;

        public Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>> Include => blogs =>
            blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => blogs => blogs.OrderBy(blog => blog.Title);
    }
}