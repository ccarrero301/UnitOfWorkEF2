namespace Shared.Patterns.Specification.Base
{
    using System;
    using System.Linq.Expressions;
    using Implementations;
    using Contracts;

    public abstract class ExpressionSpecification<TEntity> : QueryableExpressionSpecification<TEntity>,
        ISpecification<TEntity>
    {
        public virtual Expression<Func<TEntity, bool>> ToExpression() => null;

        public bool IsSatisfiedBy(TEntity entityToTest)
        {
            var predicate = ToExpression().Compile();
            return predicate(entityToTest);
        }

        public ISpecification<TEntity> And(ISpecification<TEntity> specification) =>
            new AndExpressionSpecification<TEntity>(this, specification as ExpressionSpecification<TEntity>);

        public static ISpecification<TEntity> operator &(ExpressionSpecification<TEntity> specificationLeft,
            ExpressionSpecification<TEntity> specificationRight) =>
            new AndExpressionSpecification<TEntity>(specificationLeft, specificationRight);

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification) =>
            new OrExpressionSpecification<TEntity>(this, specification as ExpressionSpecification<TEntity>);

        public static ISpecification<TEntity> operator |(ExpressionSpecification<TEntity> specificationLeft,
            ExpressionSpecification<TEntity> specificationRight) =>
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