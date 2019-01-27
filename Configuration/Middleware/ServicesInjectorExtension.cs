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
    using AppPostsContracts = AppServices.Posts.Contracts;
    using AppPostsServices = AppServices.Posts.Services;
    using DataPostsContracts = Data.Posts.Contracts;
    using DataPostsServices = Data.Posts.Services;
    using AppUsersContracts = AppServices.Users.Contracts;
    using AppUsersServices = AppServices.Users.Services;
    using DataUsersContracts = Data.Users.Contracts;
    using DataUsersServices = Data.Users.Services;
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

            services.AddScoped<AppPostsContracts.IPostService, AppPostsServices.PostService>();

            services.AddScoped<DataPostsContracts.IPostService, DataPostsServices.PostService>();

            services.AddScoped<AppUsersContracts.IUserService, AppUsersServices.UserService>();

            services.AddScoped<DataUsersContracts.IUserService, DataUsersServices.UserService>();
        }
    }
}