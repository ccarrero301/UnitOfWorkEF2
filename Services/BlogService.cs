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

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
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
                ).ConfigureAwait(false);

            return blogsPagedList.Items;
        }

        public Task<string> GetBlogTitleAsync(int blogId)
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

        public Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: null
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts)
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blog => blog.Id == blogId,
                    orderBy: null,
                    include: t => t.Include(blog => blog.Posts).ThenInclude(post => post.Comments)
                );
        }

        public Task<int> AddBlogAsync(Blog blog)
        {
            var blogRepository = _unitOfWork.GetRepository<Blog>();

            blogRepository.Insert(blog);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<int> DeleteBlogAsync(int blogId)
        {
            var blogRepository = _unitOfWork.GetRepository<Blog>();

            blogRepository.Delete(blogId);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}