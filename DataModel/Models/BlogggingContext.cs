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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasKey(blog => blog.Id);
            modelBuilder.Entity<Blog>().Property(blog => blog.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>()
                .HasMany(blog => blog.Posts)
                .WithOne(post => post.Blog)
                .HasForeignKey(post => post.BlogId);

            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Post>().Property(post => post.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.PostId);

            modelBuilder.Entity<Comment>().HasKey(t => t.Id);
            modelBuilder.Entity<Comment>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(user => user.Password)
                .HasConversion(password => UserUtils.EncryptPassword(password),
                    password => password);
            modelBuilder.Entity<User>().Ignore(user => user.Token);
        }
    }
}