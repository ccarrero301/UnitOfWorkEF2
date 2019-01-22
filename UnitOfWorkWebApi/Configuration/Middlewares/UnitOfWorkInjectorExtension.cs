namespace UnitOfWorkWebApi.Configuration.Middlewares
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.Repository;
    using UnitOfWork.Contracts.UnitOfWork;
    using UnitOfWork.Implementations;
    using InternalServices;
    using DataModel.Models;

    public static class UnitOfWorkInjectorExtension
    {
        public static void InjectUnitOfWork(this IServiceCollection services, ISettings settings)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(settings.UnitOfWorkConnectionString));

            services.AddScoped<IRepositoryFactory, UnitOfWork<BloggingContext>>();

            services.AddScoped<IUnitOfWork<BloggingContext>, UnitOfWork<BloggingContext>>();
        }
    }
}