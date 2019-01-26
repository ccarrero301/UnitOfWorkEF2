namespace BlogsWebApi.Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Requirements;
    using Data.Blogs.Contracts;
    using Data.Blogs.Services;
    using Data.Comments.Contracts;
    using Data.Comments.Services;
    using Data.Posts.Contracts;
    using Data.Posts.Services;
    using Data.Users.Contracts;
    using Data.Users.Services;

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