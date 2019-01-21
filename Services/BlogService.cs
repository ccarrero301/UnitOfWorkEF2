namespace Services
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel.Models;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.Contracts.UnitOfWork;

    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public BlogService(IUnitOfWork<BloggingContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var blogsPagedList = await blogRepository
                .GetPagedListAsync(
                    selector: blog => blog,
                    predicate: null,
                    orderBy: order => order.OrderBy(blog => blog.Id),
                    include: include => include.Include(blog => blog.Posts).ThenInclude(post => post.Comments),
                    pageIndex: 0,
                    pageSize: 20
                );

            return blogsPagedList.Items;
        }

        public Task<string> GetBlogTitle(int blogId)
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return blogRepository
                .GetFirstOrDefaultAsync(
                    selector: blog => blog.Title,
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Title)
                );
        }

        public Task<Blog> GetBlogNotIncludingPostsAndComments(int blogId)
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return blogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: null
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndNotIncludingComments(int blogId)
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return blogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts)
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndComments(int blogId)
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return blogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments)
                );
        }
    }
}
