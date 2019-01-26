namespace AppServices.Blogs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Blogs.Contracts;
    using Shared.DTOs;
    
    public class BlogService
    {
        private readonly IBlogService _blogDataService;

        public BlogService(IBlogService blogDataService)
        {
            _blogDataService = blogDataService;
        }

        //public async Task<IEnumerable<Blog>> GetAllBlogs()
        //{
        //    var dataBlogs = await _blogDataService.GetAllBlogsAsync().ConfigureAwait(false);

        //    return dataBlogs;
        //}
    }
}
