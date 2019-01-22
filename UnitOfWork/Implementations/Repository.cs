namespace UnitOfWork.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Contracts.Repository;

    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        public void Insert(TEntity entity) => DbSet.Add(entity);

        public void Insert(params TEntity[] entities) => DbSet.AddRange(entities);

        public void Insert(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);

        public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) =>
            DbSet.AddAsync(entity, cancellationToken);

        public Task InsertAsync(params TEntity[] entities) => DbSet.AddRangeAsync(entities);

        public Task InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            DbSet.AddRangeAsync(entities, cancellationToken);

        public void Attach(TEntity entity) => DbSet.Attach(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public void Update(params TEntity[] entities) => DbSet.UpdateRange(entities);

        public void Update(IEnumerable<TEntity> entities) => DbSet.UpdateRange(entities);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void Delete(object id)
        {
            var entityType = typeof(TEntity);

            var key = DbContext
                        .Model
                        .FindEntityType(entityType)
                        .FindPrimaryKey()
                        .Properties
                        .FirstOrDefault();

            var property = entityType.GetProperty(key?.Name);

            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = DbSet.Find(id);

                if (entity != null)
                    Delete(entity);
            }
        }

        public void Delete(params TEntity[] entities) => DbSet.RemoveRange(entities);

        public void Delete(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);
    }
}