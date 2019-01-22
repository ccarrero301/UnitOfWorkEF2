namespace UnitOfWorkWebApi.Configuration.Extensions.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    
    public static class MvcExtension
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            AddMvc(services);
            AddVersioning(services);
        }

        private static void AddMvc(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        private static void AddVersioning(IServiceCollection services) => services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }
}
