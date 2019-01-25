namespace DataModel.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Patterns.Specification.Base;
    using Models;

    public class BlogSpecification : ExpressionSpecification<Blog>
    {
        private readonly int _blogId;

        public BlogSpecification(int blogId) => _blogId = blogId;

        public override Expression<Func<Blog, bool>> ToExpression() => blog => blog.Id == _blogId;
    }
}