namespace Configuration.Middleware
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using InternalServices;

    internal static class SecurityExtension
    {
        internal static void UseSecurityBuilder(this IApplicationBuilder applicationBuilder,
            IHostingEnvironment environment, ILog log)
        {
            applicationBuilder.UseHttpsRedirection();

            applicationBuilder.UseAuthentication();

            applicationBuilder.UseHsts();
        }
    }
}