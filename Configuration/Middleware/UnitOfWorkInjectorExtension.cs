namespace Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Implementations;
    using Patterns.Settings;
    using Data;

    internal static class UnitOfWorkInjectorExtension
    {
        internal static void InjectUnitOfWork(this IServiceCollection services, ISettings settings)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(settings.UnitOfWorkConnectionString));

            services.AddUnitOfWork<BloggingContext>();
        }
    }
}