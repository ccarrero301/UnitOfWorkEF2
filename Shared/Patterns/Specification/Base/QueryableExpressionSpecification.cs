namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq.Expressions;
    using Implementations;
    using Contracts;

    public abstract class QueryableExpressionSpecification<TEntity> : QueryableSpecification<TEntity>, ISpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entityToTest)
        {
            var predicate = ToExpression().Compile();
            return predicate(entityToTest);
        }

        public ISpecification<TEntity> And(ISpecification<TEntity> specification) =>
            new AndExpressionSpecification<TEntity>(this, specification as QueryableExpressionSpecification<TEntity>);

        public static ISpecification<TEntity> operator &(QueryableExpressionSpecification<TEntity> specificationLeft,
            QueryableExpressionSpecification<TEntity> specificationRight) =>
            new AndExpressionSpecification<TEntity>(specificationLeft, specificationRight);

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification) =>
            new OrExpressionSpecification<TEntity>(this, specification as QueryableExpressionSpecification<TEntity>);

        public static ISpecification<TEntity> operator |(QueryableExpressionSpecification<TEntity> specificationLeft,
            QueryableExpressionSpecification<TEntity> specificationRight) =>
            new OrExpressionSpecification<TEntity>(specificationLeft, specificationRight);

        public ISpecification<TEntity> Not()
        {
            throw new NotImplementedException();
        }

        public ISpecification<TEntity> All()
        {
            throw new NotImplementedException();
        }
    }
}