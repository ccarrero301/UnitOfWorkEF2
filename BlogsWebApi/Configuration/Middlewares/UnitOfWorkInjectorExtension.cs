namespace BlogsWebApi.Configuration.Middlewares
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Implementations;
    using Patterns.Settings;
    using DataModel.Models;

    public static class UnitOfWorkInjectorExtension
    {
        public static void InjectUnitOfWork(this IServiceCollection services, ISettings settings)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(settings.UnitOfWorkConnectionString));

            services.AddUnitOfWork<BloggingContext>();
        }
    }
}