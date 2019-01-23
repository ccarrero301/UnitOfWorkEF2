namespace UnitOfWorkWebApi.Configuration.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Filters;
    using Newtonsoft.Json;

    public static class MvcExtension
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            AddMvc(services);

            AddVersioning(services);
        }

        private static void AddMvc(IServiceCollection services) =>
            services
                .AddMvc(options => options.Filters.Add(new ValidateModelActionFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        private static void AddVersioning(IServiceCollection services) => services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

        public static void UseMvcBuilder(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMvc();
    }
}