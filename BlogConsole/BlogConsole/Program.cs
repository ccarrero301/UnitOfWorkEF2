namespace BlogConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Implementations;
    using UnitOfWork.Contracts.UnitOfWork;
    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            //Task.Run(AddBlogAsync);

            //GetData();

            //Task.Run(GetDataAsync);

            //FindAndUpdate();

            //FindAndUpdateAsync().Wait();
        }

        private static async Task AddBlogAsync()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var blogRepository = unitOfWork.GetRepository<Blog>();

                await blogRepository.InsertAsync(GetSampleBlog());

                await unitOfWork.SaveChangesAsync();
            }
        }

        private static void GetData()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();

                var blogList = queryableRepository
                    .GetPagedList(orderBy: t => t.OrderBy(blog => blog.Id),
                        include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments));
            }
        }

        private static async Task GetDataAsync()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();

                var blogList = await queryableRepository
                    .GetPagedListAsync(orderBy: t => t.OrderBy(blog => blog.Id),
                        include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments));
            }
        }

        private static void FindAndUpdate()
        {
            Blog selectedBlog;

            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();
                selectedBlog = queryableRepository.Find(1);
            }

            using (var unitOfWork = GetUnitOfWork())
            {
                var repository = unitOfWork.GetRepository<Blog>();
                selectedBlog.Title = "My title sync 123";

                repository.Update(selectedBlog);

                unitOfWork.SaveChanges();
            }
        }

        private static async Task FindAndUpdateAsync()
        {
            Blog selectedBlog;

            using (var unitOfWork = GetUnitOfWork())
            {
                var queryableRepository = unitOfWork.GetQueryableRepository<Blog>();
                selectedBlog = await queryableRepository.FindAsync(1);
            }

            using (var unitOfWork = GetUnitOfWork())
            {
                var repository = unitOfWork.GetRepository<Blog>();
                selectedBlog.Title = "My title sync 123";

                repository.Update(selectedBlog);

                await unitOfWork.SaveChangesAsync();
            }
        }

        private static IUnitOfWork<BloggingContext> GetUnitOfWork() => new UnitOfWork<BloggingContext>(
            new DesignTimeDbContextFactory().CreateDbContext(
                args: null));

        private static Blog GetSampleBlog()
        {
            return new Blog
            {
                Url = "/a/" + 1,
                Title = $"a{1}",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "A",
                        Content = "A's content",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "A",
                                Content = "A's content",
                            },
                            new Comment
                            {
                                Title = "b",
                                Content = "b's content",
                            },
                            new Comment
                            {
                                Title = "c",
                                Content = "c's content",
                            }
                        },
                    },
                    new Post
                    {
                        Title = "B",
                        Content = "B's content",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "A",
                                Content = "A's content",
                            },
                            new Comment
                            {
                                Title = "b",
                                Content = "b's content",
                            },
                            new Comment
                            {
                                Title = "c",
                                Content = "c's content",
                            }
                        },
                    },
                    new Post
                    {
                        Title = "C",
                        Content = "C's content",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "A",
                                Content = "A's content",
                            },
                            new Comment
                            {
                                Title = "b",
                                Content = "b's content",
                            },
                            new Comment
                            {
                                Title = "c",
                                Content = "c's content",
                            }
                        },
                    },
                    new Post
                    {
                        Title = "D",
                        Content = "D's content",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Title = "A",
                                Content = "A's content",
                            },
                            new Comment
                            {
                                Title = "b",
                                Content = "b's content",
                            },
                            new Comment
                            {
                                Title = "c",
                                Content = "c's content",
                            }
                        },
                    }
                },
            };
        }
    }
}