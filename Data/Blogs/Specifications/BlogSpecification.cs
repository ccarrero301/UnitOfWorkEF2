namespace Data.Blogs.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;
    
    public class BlogSpecification : QueryableExpressionSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;
    }
}