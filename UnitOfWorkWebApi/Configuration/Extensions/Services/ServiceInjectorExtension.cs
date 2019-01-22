namespace UnitOfWorkWebApi.Configuration.Extensions.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using global::Services;

    public static class ServiceInjectorExtension
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ICommentService, CommentService>();
        }
    }
}
