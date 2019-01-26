namespace Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using AppBlogsContracts = AppServices.Blogs.Contracts;
    using AppBlogsServices = AppServices.Blogs.Services;
    using DataBlogsContracts = Data.Blogs.Contracts;
    using DataBlogsServices = Data.Blogs.Services;
    using Data.Comments.Contracts;
    using Data.Comments.Services;
    using Data.Posts.Contracts;
    using Data.Posts.Services;
    using Data.Users.Contracts;
    using Data.Users.Services;
    using Requirements;

    internal static class ServicesInjectorExtension
    {
        internal static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IAuthorizationHandler, B2BRequirementHandler>();

            services.AddScoped<AppBlogsContracts.IBlogService, AppBlogsServices.BlogService>();

            services.AddScoped<DataBlogsContracts.IBlogService, DataBlogsServices.BlogService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}