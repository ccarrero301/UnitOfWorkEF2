namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts.Repository;

    public class BlogSpecification : IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogSpecification(int blogId) => _blogId = blogId;

        public Expression<Func<Blog, bool>> Predicate => blog => blog.Id == _blogId;

        public Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;
    }
}