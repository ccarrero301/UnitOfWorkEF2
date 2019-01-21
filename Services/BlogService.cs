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
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var blogsPagedList = await queryableBlogRepository
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
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    selector: blog => blog.Title,
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Title)
                );
        }

        public Task<Blog> GetBlogNotIncludingPostsAndComments(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: null
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndNotIncludingComments(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts)
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndComments(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments)
                );
        }

        public async Task<int> AddPostToBlog(int blogId, Post post)
        {
            var blogRepository = _unitOfWork.GetRepository<Blog>();

            var blog = await GetBlogIncludingPostsAndComments(blogId);

            blogRepository.Attach(blog);

            var posts = blog.Posts.ToList();
            posts.Add(post);

            blog.Posts = posts;

            blogRepository.Update(blog);

            return await _unitOfWork.SaveChangesAsync(true).ConfigureAwait(false);
        }
    }
}