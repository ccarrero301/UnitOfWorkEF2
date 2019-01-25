namespace Services.Blogs
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using UnitOfWork.Contracts.Repository;
    using DataModel.Models;

    public class BlogSpecification : IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogSpecification(int blogId) => _blogId = blogId;

        public Expression<Func<Blog, bool>> Predicate => blog => blog.Id == _blogId;

        public Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>> Include => null;

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;
    }
}
