namespace Services
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DataModel.Specifications;
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
            var allBlogsWithPostsSpecification = new AllBlogsWithPostsSpecification();

            var blogsPagedList = await queryableBlogRepository
                .GetPagedListAsync(
                    selector: blog => blog,
                    predicate: allBlogsWithPostsSpecification,
                    orderBy: order => order.OrderBy(blog => blog.Id),
                    pageIndex: 0,
                    pageSize: 20
                ).ConfigureAwait(false);

            return blogsPagedList.Items;
        }

        public Task<string> GetBlogTitleAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();
            var blogTitleSpecification = new BlogTitleSpecification(blogId);

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    selector: blog => blog.Title,
                    predicate: blogTitleSpecification,
                    orderBy: null
                );
        }

        public Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();
            var blogNotPostAndCommentsSpecification = new BlogSpecification(blogId);

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blogNotPostAndCommentsSpecification,
                    orderBy: null
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();
            var blogPostsAndNoCommentsSpecification = new BlogPostsAndNoCommentsSpecification(blogId);

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blogPostsAndNoCommentsSpecification,
                    orderBy: null
                );
        }

        public Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();
            var blogPostsAndCommentsSpecification = new BlogPostsAndCommentsSpecification(blogId);

            return queryableBlogRepository
                .GetFirstOrDefaultAsync(
                    predicate: blogPostsAndCommentsSpecification,
                    orderBy: null
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