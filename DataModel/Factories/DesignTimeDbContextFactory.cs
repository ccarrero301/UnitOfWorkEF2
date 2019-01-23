namespace DataModel.Factories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Models;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        public BloggingContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BloggingContext>();

            const string connectionString =
                @"Server=(localdb)\mssqllocaldb;Database=Blogs.AspNetCore;Trusted_Connection=True;";

            builder.UseSqlServer(connectionString);

            return new BloggingContext(builder.Options);
        }
    }
}