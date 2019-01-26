namespace AppServices.Blogs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataBlogs = Data.Blogs.Contracts;
    using Shared.DTOs;
    using Contracts;
    using AutoMapper;
    
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly DataBlogs.IBlogService _blogService;

        public BlogService(IMapper mapper, DataBlogs.IBlogService blogService)
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
    }
}
