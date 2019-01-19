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
    using DataModel.Factories;
    using DataModel.Models;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //await AddBlogAsync();

            //var blogs = await GetAllBlogs(); 
            //PrintBlogsPostAndComments(blogs.Items);

            //var posts = await GetPostsThatHaveTheWordHuskyInPostContent();
            //PrintPosts(posts.Items);

            //var blogs = await GetBlogsThatHaveTheWordAmInCommentContent();
            //PrintBlogsPostAndComments(blogs);

            //var oneBlog = GetBlog();

            var oneBlogTitle = GetBlogTitle();

            //await UpdateBlogByIdAsyncWithAutoHistory(8);

            Console.WriteLine("Press Key to Enter...");
            Console.ReadKey();
        }

        #region Unit Of Work

        private static IUnitOfWork<BloggingContext> GetUnitOfWork() =>
            new UnitOfWork<BloggingContext>(new DesignTimeDbContextFactory().CreateDbContext(null));

        #endregion

        #region Commands

        private static async Task AddBlogAsync()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var blogRepository = unitOfWork.GetRepository<Blog>();

                await blogRepository.InsertAsync(GetSampleBlog()).ConfigureAwait(false);

                await unitOfWork.SaveChangesAsync(true).ConfigureAwait(false);
            }
        }

        private static async Task UpdateBlogByIdAsyncWithNoAutoHistory(int blogId)
        {
            Blog selectedBlog;

            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();
                selectedBlog = await queryableRepository.FindAsync(blogId).ConfigureAwait(false);
            }

            selectedBlog.Title = "This blog is about dogs and cats 2!";

            using (var unitOfWork = GetUnitOfWork())
            {
                var repository = unitOfWork.GetRepository<Blog>();

                repository.Update(selectedBlog);

                await unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private static async Task UpdateBlogByIdAsyncWithAutoHistory(int blogId)
        {          
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();
                var selectedBlog = await queryableRepository.FindAsync(blogId).ConfigureAwait(false);

                selectedBlog.Title = "This blog is about dogs and cats 3!";

                var repository = unitOfWork.GetRepository<Blog>();

                repository.Update(selectedBlog);

                await unitOfWork.SaveChangesAsync(true).ConfigureAwait(false);
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
                    .GetPagedListAsync(
                        selector: blog => blog,
                        predicate: null,
                        orderBy: order => order.OrderBy(blog => blog.Id),
                        include: include => include.Include(blog => blog.Posts).ThenInclude(post => post.Comments),
                        pageIndex: 0,
                        pageSize: 20
                    ).ConfigureAwait(false);
            }
        }

        private static async Task<IPagedList<Post>> GetPostsThatHaveTheWordHuskyInPostContent()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Post>();

                return await queryableRepository
                    .GetPagedListAsync(
                        selector: post => post,
                        predicate: post => post.Content.Contains("Huskies"),
                        orderBy: t => t.OrderBy(post => post.Id),
                        include: t => t.Include(post => post.Comments),
                        pageIndex: 0,
                        pageSize: 20
                    ).ConfigureAwait(false);
            }
        }

        private static async Task<IEnumerable<Blog>> GetBlogsThatHaveTheWordAmInCommentContent()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Comment>();

                var comments = await queryableRepository
                    .GetPagedListAsync(
                        selector: comment => comment,
                        predicate: comment => comment.Content == "Hi there... I'm crazy",
                        orderBy: t => t.OrderBy(post => post.Id),
                        include: t => t.Include(comment => comment.Post).ThenInclude(post => post.Blog),
                        pageIndex: 0,
                        pageSize: 20
                    ).ConfigureAwait(false);

                var blogs = comments.Items.Select(comment => comment.Post.Blog).ToList();

                return blogs;
            }
        }

        private static Blog GetBlog()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();

                return queryableRepository
                    .GetFirstOrDefault(
                        selector: blog => blog,
                        predicate: blog => blog.Id == 8,
                        orderBy: t => t.OrderBy(blog => blog.Id),
                        include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments)
                    );
            }
        }

        private static string GetBlogTitle()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();

                return queryableRepository
                    .GetFirstOrDefault(
                        selector: blog => blog.Title,
                        predicate: blog => blog.Id == 8,
                        orderBy: t => t.OrderBy(blog => blog.Id),
                        include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments)
                    );
            }
        }

        #endregion

        #region Output Utilities

        private static void PrintBlogsPostAndComments(IEnumerable<Blog> blogs)
        {
            Console.ResetColor();
            Console.WriteLine($"*****************BLOGS - POSTS - COMMENTS***********************");

            foreach (var blog in blogs)
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

            Console.ResetColor();
            Console.WriteLine($"********************************************************************");
        }

        private static void PrintPosts(IEnumerable<Post> posts)
        {
            Console.ResetColor();
            Console.WriteLine($"*****************POSTS***********************");

            foreach (var post in posts)
            {
                PrintPost(post);
            }

            Console.ResetColor();
            Console.WriteLine($"********************************************************************");
        }

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