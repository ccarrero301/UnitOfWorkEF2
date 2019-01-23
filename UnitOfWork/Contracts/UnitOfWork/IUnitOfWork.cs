namespace UnitOfWork.Contracts.UnitOfWork
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Repository;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        IQueryableRepository<TEntity> GetQueryableRepository<TEntity>() where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}