namespace Services.Blogs
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using UnitOfWork.Contracts.Repository;
    using DataModel.Models;

    public class BlogWithPostsSpecification : IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogWithPostsSpecification(int blogId) => _blogId = blogId;

        public Expression<Func<Blog, bool>> Predicate => blog => blog.Id == _blogId;

        public Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>> Include => blogs =>
            blogs.Include(blog => blog.Posts);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;
    }
}
