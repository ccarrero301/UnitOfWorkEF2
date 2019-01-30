namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts.Repository;
    using Shared.Patterns.Specification.Base;
    
    public class BlogSpecification : ExpressionSpecification<Blog>, IQueryableSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;

        public Expression<Func<Blog, bool>> Predicate => ToExpression();

        public Func<IQueryable<Blog>, TIncludableQueryable> Include<TIncludableQueryable>() => null;

        public Func<IQueryable<Blog>, IOrderedQueryable<Blog>> OrderBy => null;

    }
}