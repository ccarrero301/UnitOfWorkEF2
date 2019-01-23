namespace UnitOfWorkWebApi.Configuration.Middlewares
{
    using Microsoft.Extensions.DependencyInjection;
    using InternalServices;
    using Services;

    public static class ServicesInjectorExtension
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}