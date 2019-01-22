namespace UnitOfWorkWebApi.Configuration.Builders
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Extensions.Middlewares;
    using InternalServices;
    
    public static class ApplicationBuilder
    {
        public static void ConfigureApplication(this IApplicationBuilder applicationBuilder,
            IHostingEnvironment environment, ILog log)
        {
            applicationBuilder.UseSecurityBuilder(environment, log);

            applicationBuilder.UseCustomExceptionHandlerBuilder(log);

            applicationBuilder.UseMvcBuilder();
        }
    }
}