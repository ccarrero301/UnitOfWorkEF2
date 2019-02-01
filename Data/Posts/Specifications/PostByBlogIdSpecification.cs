namespace Data.Posts.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;

    public class PostByBlogIdSpecification : QueryableExpressionSpecification<Post>
    {
        private readonly int _blogId;

        public PostByBlogIdSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Post, bool>> ToExpression() => post => post.BlogId == _blogId;
    }
}