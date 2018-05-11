namespace UnitOfWork.Contracts.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IQueryableRepository<TEntity> GetQueryableRepository<TEntity>() where TEntity : class;
    }
}