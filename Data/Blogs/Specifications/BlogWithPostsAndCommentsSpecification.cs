namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;
    
    public class BlogWithPostsAndCommentsSpecification : ExpressionSpecification<Blog>, IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogWithPostsAndCommentsSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;

        public Expression<Func<Blog, bool>> Predicate => ToExpression();

        public Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable) blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;
    }
}