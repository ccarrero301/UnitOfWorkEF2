﻿namespace Data.Posts.Specifications
{
    using System;
    using System.Linq.Expressions;
    using Shared.Patterns.Specification.Base;

    public class PostContainsWordInContentSpecification : ExpressionSpecification<Post>
    {
        private readonly string _word;

        public PostContainsWordInContentSpecification(string word) => _word = word;

        public override Expression<Func<Post, bool>> ToExpression() => post => post.Content.Contains(_word);
    }
}