namespace AppServices.Blogs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataBlogsContracts = Data.Blogs.Contracts;
    using DomainBlogs = Domain.Blogs;
    using Shared.DTOs;
    using Contracts;
    using AutoMapper;

    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly DataBlogsContracts.IBlogService _blogService;

        public BlogService(IMapper mapper, DataBlogsContracts.IBlogService blogService)
        {
            _mapper = mapper;
            _blogService = blogService;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            var dataBlogs = await _blogService.GetAllBlogsAsync().ConfigureAwait(false);

            var dtoBlogs = _mapper.Map<IEnumerable<Blog>>(dataBlogs);

            return dtoBlogs;
        }

        public Task<string> GetBlogTitleAsync(int blogId) => _blogService.GetBlogTitleAsync(blogId);

        public async Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var dataBlog = await _blogService.GetBlogNotIncludingPostsAndCommentsAsync(blogId).ConfigureAwait(false);

            var dtoBlog = _mapper.Map<Blog>(dataBlog);

            return dtoBlog;
        }

        public async Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var dataBlog = await _blogService.GetBlogIncludingPostsAndNotIncludingCommentsAsync(blogId)
                .ConfigureAwait(false);

            var dtoBlog = _mapper.Map<Blog>(dataBlog);

            return dtoBlog;
        }

        public async Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var dataBlog = await _blogService.GetBlogIncludingPostsAndCommentsAsync(blogId).ConfigureAwait(false);

            var dtoBlog = _mapper.Map<Blog>(dataBlog);

            return dtoBlog;
        }

        public Task<int> AddBlogAsync(Blog blog)
        {
            var domainBlog = _mapper.Map<DomainBlogs.Blog>(blog);

            return _blogService.AddBlogAsync(domainBlog);
        }

        public Task<int> DeleteBlogAsync(int blogId) => _blogService.DeleteBlogAsync(blogId);
    }
}
