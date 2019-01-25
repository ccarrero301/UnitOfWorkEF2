namespace Services.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DataModel.Models;
    
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
                    specification: new AllBlogsSpecification(),
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
                    specification: new BlogTitleSpecification(blogId)
                );
        }

        public Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    specification: new BlogSpecification(blogId)
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    specification: new BlogWithPostsSpecification(blogId)
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    specification: new BlogWithPostsAndCommentsSpecification(blogId)
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