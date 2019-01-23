namespace UnitOfWork.Contracts.UnitOfWork
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }

        Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks);
    }
}