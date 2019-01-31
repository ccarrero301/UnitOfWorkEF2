﻿// <auto-generated />

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(BloggingContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Blogs.Blog", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Title");

                b.Property<string>("Url");

                b.HasKey("Id");

                b.ToTable("Blogs");
            });

            modelBuilder.Entity("Data.Comments.Comment", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Content");

                b.Property<int>("PostId");

                b.Property<string>("Title");

                b.HasKey("Id");

                b.HasIndex("PostId");

                b.ToTable("Comments");
            });

            modelBuilder.Entity("Data.Posts.Post", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("BlogId");

                b.Property<string>("Content");

                b.Property<string>("Title");

                b.HasKey("Id");

                b.HasIndex("BlogId");

                b.ToTable("Posts");
            });

            modelBuilder.Entity("Data.Users.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("FirstName");

                b.Property<string>("LastName");

                b.Property<string>("Password");

                b.Property<string>("Role");

                b.Property<string>("Username");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Data.Comments.Comment", b =>
            {
                b.HasOne("Data.Posts.Post", "Post")
                    .WithMany("Comments")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Data.Posts.Post", b =>
            {
                b.HasOne("Data.Blogs.Blog", "Blog")
                    .WithMany("Posts")
                    .HasForeignKey("BlogId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}