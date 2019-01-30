namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;
    
    public class AllBlogsSpecification : ExpressionSpecification<Blog>, IQueryableSpecification<Blog>
    {
        public override Expression<Func<Blog, bool>> ToExpression() => null;

        public Expression<Func<Blog, bool>> Predicate => ToExpression();

        public Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => blogs =>
            (TIncludableQueryable) blogs.Include(blog => blog.Posts).ThenInclude(post => post.Comments);

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => blogs => blogs.OrderBy(blog => blog.Title);
    }
}