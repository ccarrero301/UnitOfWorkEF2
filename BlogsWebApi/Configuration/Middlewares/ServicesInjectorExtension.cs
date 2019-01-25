namespace BlogsWebApi.Configuration.Middlewares
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Requirements;
    using Services.Blogs;
    using Services.Users;
    using Services.Posts;
    using Services.Comments;

    public static class ServicesInjectorExtension
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IAuthorizationHandler, B2BRequirementHandler>();

            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}