namespace Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using AppBlogsContracts = AppServices.Blogs.Contracts;
    using AppBlogsServices = AppServices.Blogs.Services;
    using DataBlogsContracts = Data.Blogs.Contracts;
    using DataBlogsServices = Data.Blogs.Services;
    using AppCommentsContracts = AppServices.Comments.Contracts;
    using AppCommentsServices = AppServices.Comments.Services;
    using DataCommentsContracts = Data.Comments.Contracts;
    using DataCommentsServices = Data.Comments.Services;
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

            services.AddScoped<AppCommentsContracts.ICommentService, AppCommentsServices.CommentService>();

            services.AddScoped<DataCommentsContracts.ICommentService, DataCommentsServices.CommentService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}