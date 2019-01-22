namespace UnitOfWorkWebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using UnitOfWork.Contracts.Repository;
    using UnitOfWork.Contracts.UnitOfWork;
    using UnitOfWork.Implementations;
    using DataModel.Models;
    using Services;
    using Newtonsoft.Json;
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=Blogs.AspNetCore;Trusted_Connection=True;"));

            services.AddScoped<IRepositoryFactory, UnitOfWork<BloggingContext>>();
            services.AddScoped<IUnitOfWork<BloggingContext>, UnitOfWork<BloggingContext>>();

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}