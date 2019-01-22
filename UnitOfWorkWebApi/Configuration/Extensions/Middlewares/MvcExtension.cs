namespace UnitOfWorkWebApi.Configuration.Extensions.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class MvcExtension
    {
        public static void UseMvcBuilder(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMvc();
    }
}