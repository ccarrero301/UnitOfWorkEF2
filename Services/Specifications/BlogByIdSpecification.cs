namespace Services.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Patterns.Specification.Base;
    using DataModel.Models;

    public class BlogByIdSpecification : ExpressionSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogByIdSpecification(int blogId)
        {
            _blogId = blogId;
        }

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;
    }
}