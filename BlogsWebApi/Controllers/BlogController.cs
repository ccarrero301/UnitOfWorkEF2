namespace BlogsWebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Data.Blogs;
    using Data.Blogs.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService) => _blogService = blogService;

        [HttpGet]
        [Route("all")]
        [Produces(typeof(IEnumerable<Blog>))]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync().ConfigureAwait(false);

            return Ok(blogs);
        }

        [HttpGet]
        [Route("{id:int}/title")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetBlogTitleById(int id)
        {
            var blogTitle = await _blogService.GetBlogTitleAsync(id).ConfigureAwait(false);

            return Ok(blogTitle);
        }

        [HttpGet]
        [Route("{id:int}/noPostsAndNoComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogNotIncludingPostsAndComments(int id)
        {
            var blog = await _blogService.GetBlogNotIncludingPostsAndCommentsAsync(id).ConfigureAwait(false);

            return Ok(blog);
        }

        [HttpGet]
        [Route("{id:int}/postsAndNoComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogIncludingPostsAndNotIncludingComments(int id)
        {
            var blog = await _blogService.GetBlogIncludingPostsAndNotIncludingCommentsAsync(id).ConfigureAwait(false);

            return Ok(blog);
        }

        [HttpGet]
        [Route("{id:int}/postsAndComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogIncludingPostsAndComments(int id)
        {
            var blog = await _blogService.GetBlogIncludingPostsAndCommentsAsync(id).ConfigureAwait(false);

            return Ok(blog);
        }

        [HttpPost]
        [Route("Add")]
        [Produces(typeof(int))]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            var numberOfRecords = await _blogService.AddBlogAsync(blog).ConfigureAwait(false);

            return Ok(numberOfRecords);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Produces(typeof(int))]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var numberOfRecords = await _blogService.DeleteBlogAsync(id).ConfigureAwait(false);

            return Ok(numberOfRecords);
        }
    }
}