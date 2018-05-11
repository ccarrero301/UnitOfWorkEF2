using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogConsole.Models
{
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
            modelBuilder.EnableAutoHistory();

            modelBuilder.Entity<Blog>().HasKey(t => t.Id);
            modelBuilder.Entity<Blog>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>()
                .HasMany(blog => blog.Posts)
                .WithOne(post => post.Blog)
                .HasForeignKey(post => post.BlogId);

            modelBuilder.Entity<Post>().HasKey(t => t.Id);
            modelBuilder.Entity<Post>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.Post)
                .HasForeignKey(t => t.PostId);

            modelBuilder.Entity<Comment>().HasKey(t => t.Id);
            modelBuilder.Entity<Comment>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}

public class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}