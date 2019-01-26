namespace Data.Blogs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using Specifications;
    using Contracts;
    using Domain;
    using AutoMapper;

    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;
        private readonly IMapper _mapper;

        public BlogService(IMapper mapper, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Blogs.Blog>> GetAllBlogsAsync()
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlogsPagedList = await queryableBlogRepository
                .GetPagedListAsync(
                    selector: blog => blog,
                    specification: new AllBlogsSpecification(),
                    pageIndex: 0,
                    pageSize: 20
                ).ConfigureAwait(false);

            var domainBlogs = _mapper.Map<IEnumerable<Domain.Blogs.Blog>>(dataBlogsPagedList.Items);

            return domainBlogs;
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

        public async Task<Domain.Blogs.Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog = await queryableBlogRepository
                .GetFirstOrDefaultAsync(specification: new BlogSpecification(blogId)).ConfigureAwait(false);

            var domainBlog = _mapper.Map<Domain.Blogs.Blog>(dataBlog);

            return domainBlog;
        }

        public async Task<Domain.Blogs.Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog = await queryableBlogRepository
                .GetFirstOrDefaultAsync(specification: new BlogWithPostsSpecification(blogId)).ConfigureAwait(false);

            var domainBlog = _mapper.Map<Domain.Blogs.Blog>(dataBlog);

            return domainBlog;
        }

        public async Task<Domain.Blogs.Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog =
                await queryableBlogRepository.GetFirstOrDefaultAsync(
                    specification: new BlogWithPostsAndCommentsSpecification(blogId));

            var domainBlog = _mapper.Map<Domain.Blogs.Blog>(dataBlog);

            return domainBlog;
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