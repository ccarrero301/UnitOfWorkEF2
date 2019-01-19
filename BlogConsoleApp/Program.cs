namespace BlogConsoleApp
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.PagedList;
    using UnitOfWork.Contracts.UnitOfWork;
    using UnitOfWork.Implementations;
    using Factories;
    using Models;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //await AddBlogAsync();

            await PrintDataAsync();

            Console.ResetColor();
            Console.WriteLine("Press Key to Enter...");
            Console.ReadKey();
        }

        #region Commands

        private static async Task AddBlogAsync()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var blogRepository = unitOfWork.GetRepository<Blog>();

                await blogRepository.InsertAsync(GetSampleBlog()).ConfigureAwait(false);

                await unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private static Blog GetSampleBlog()
        {
            return new Blog
            {
                Url = "dogs.com",
                Title = "This blog is about dogs!",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Huskies",
                        Content = "Huskies are amazing",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "I am Kira",
                                Content = "Hi there... I'm crazy",
                            },
                            new Comment
                            {
                                Title = "I am Fluffy",
                                Content = "What's up, I have a lot of hair!",
                            },
                            new Comment
                            {
                                Title = "I am Snow",
                                Content = "I fancy white stuff",
                            }
                        },
                    },
                    new Post
                    {
                        Title = "Border Collies",
                        Content = "Collies are beautiful",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "I am Chelsea",
                                Content = "I have a bad mood!"
                            },
                            new Comment
                            {
                                Title = "My name is Thunder",
                                Content = "I am pretty fast..."
                            },
                            new Comment
                            {
                                Title = "They call me Noisy",
                                Content = "I bark a lot!!!"
                            }
                        },
                    },
                    new Post
                    {
                        Title = "German Shephards",
                        Content = "We are watching!",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "My name is Pretty",
                                Content = "I am a girl!",
                            },
                            new Comment
                            {
                                Title = "I am Sussy",
                                Content = "I am very brave!",
                            },
                            new Comment
                            {
                                Title = "I am Doggy",
                                Content = "I am the perfect dog!"
                            }
                        },
                    }
                },
            };
        }

        #endregion

        #region Queries

        private static async Task<IPagedList<Blog>> GetAllBlogs()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();

                return await queryableRepository
                    .GetPagedListAsync(predicate: null,
                        orderBy: t => t.OrderBy(blog => blog.Id),
                        include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments),
                        pageIndex: 0,
                        pageSize: 20
                    ).ConfigureAwait(false);
            }
        }

        private static async Task<IPagedList<Post>> GetPostsThatHaveTheWordHusky()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Post>();

                return await queryableRepository
                    .GetPagedListAsync(predicate: post => post.Content.Contains("Huskies"),
                        orderBy: t => t.OrderBy(post => post.Id),
                        include: t => t.Include(post => post.Comments),
                        pageIndex: 0,
                        pageSize: 20
                    ).ConfigureAwait(false);
            }
        }

        #endregion

        #region Entry Point Methods

        private static async Task PrintDataAsync()
        {
            var blogs = await GetAllBlogs().ConfigureAwait(false);

            foreach (var blog in blogs.Items)
            {
                PrintBlog(blog);

                foreach (var post in blog.Posts)
                {
                    PrintPost(post);

                    foreach (var comment in post.Comments)
                    {
                        PrintComment(comment);
                    }
                }
            }
        }

        #endregion

        #region Unit Of Work

        private static IUnitOfWork<BloggingContext> GetUnitOfWork() =>
            new UnitOfWork<BloggingContext>(new DesignTimeDbContextFactory().CreateDbContext(null));

        #endregion

        #region Output Utilities

        private static void PrintBlog(Blog blog)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine($"****************************************");
            Console.WriteLine($"Blog Id :\t {blog.Id}");
            Console.WriteLine($"Blog Title :\t {blog.Title}");
            Console.WriteLine($"Blog Url :\t {blog.Url}");
            Console.WriteLine($"****************************************");
        }

        private static void PrintPosts(IEnumerable<Post> posts)
        {
            foreach (var post in posts)
            {
                PrintPost(post);
            }
        }

        private static void PrintPost(Post post)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"\t****************************************");
            Console.WriteLine($"\tBlog Id :\t {post.BlogId}");
            Console.WriteLine($"\tPost Id :\t {post.Id}");
            Console.WriteLine($"\tPost Title :\t {post.Title}");
            Console.WriteLine($"\tPost Content :\t {post.Content}");
            Console.WriteLine($"\t****************************************");
        }

        private static void PrintComment(Comment comment)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"\t\t****************************************");
            Console.WriteLine($"\t\tPost Id :\t\t {comment.PostId}");
            Console.WriteLine($"\t\tComment Id :\t\t {comment.Id}");
            Console.WriteLine($"\t\tComment Title :\t\t {comment.Title}");
            Console.WriteLine($"\t\tComment :\t\t {comment.Content}");
            Console.WriteLine($"\t\t****************************************");
        }

        #endregion
    }
}
