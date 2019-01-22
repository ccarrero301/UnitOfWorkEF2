namespace UnitOfWorkWebApi.Configuration.Builders
{
    using Microsoft.Extensions.DependencyInjection;
    using Extensions.Services;
    using InternalServices;
    
    public static class ServicesBuilder
    {
        public static void ConfigureServices(this IServiceCollection services, ISettings settings)
        {
            services.InjectUnitOfWork(settings);

            services.InjectServices();

            services.ConfigureMvc();
        }
    }
}
