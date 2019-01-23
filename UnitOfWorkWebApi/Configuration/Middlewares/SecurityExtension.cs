namespace UnitOfWorkWebApi.Configuration.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using InternalServices;

    public static class SecurityExtension
    {
        public static void UseSecurityBuilder(this IApplicationBuilder applicationBuilder,
            IHostingEnvironment environment, ILog log)
        {
            applicationBuilder.UseHttpsRedirection();

            applicationBuilder.UseHsts();
        }
    }
}