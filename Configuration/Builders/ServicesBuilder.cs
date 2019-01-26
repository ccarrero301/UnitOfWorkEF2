namespace Configuration.Builders
{
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Patterns.Settings;
    
    public static class ServicesBuilder
    {
        public static void ConfigureServices(this IServiceCollection services, ISettings settings)
        {
            services.ConfigureAuthentication(settings);

            services.ConfigureAuthorization(settings);

            services.InjectUnitOfWork(settings);

            services.InjectServices();

            services.ConfigureSwagger();

            services.ConfigureAutoMapper();

            services.ConfigureMvc();
        }
    }
}