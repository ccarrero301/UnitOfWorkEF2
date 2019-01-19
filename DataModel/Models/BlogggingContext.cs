namespace DataModel.Models
{
    using Microsoft.EntityFrameworkCore;

    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory(null);

            modelBuilder.Entity<Blog>().HasKey(t => t.Id);
            modelBuilder.Entity<Blog>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>()
                .HasMany(blog => blog.Posts)
                .WithOne(post => post.Blog)
                .HasForeignKey(post => post.BlogId);

            modelBuilder.Entity<Post>().HasKey(t => t.Id);
            modelBuilder.Entity<Post>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.PostId);

            modelBuilder.Entity<Comment>().HasKey(t => t.Id);
            modelBuilder.Entity<Comment>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}