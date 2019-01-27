namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.Repository;

    public class BlogWithPostsAndCommentsSpecification : IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogWithPostsAndCommentsSpecification(int blogId) => _blogId = blogId;

        public Expression<Func<Blog, bool>> Predicate => blog => blog.Id == _blogId;

        public Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable)blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;
    }
}
