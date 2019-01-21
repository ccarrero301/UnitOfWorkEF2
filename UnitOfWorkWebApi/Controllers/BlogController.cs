namespace UnitOfWorkWebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("All")]
        [Produces(typeof(IEnumerable<Blog>))]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogs();

            return Ok(blogs.ToList());
        }

        [HttpGet]
        [Route("{id:int}/title")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetBlogTitleById(int id)
        {
            var blogTitle = await _blogService.GetBlogTitle(id);

            return Ok(blogTitle);
        }

        [HttpGet]
        [Route("{id:int}/noPostsAndNoComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogNotIncludingPostsAndComments(int id)
        {
            var blog = await _blogService.GetBlogNotIncludingPostsAndComments(id);

            return Ok(blog);
        }

        [HttpGet]
        [Route("{id:int}/postsAndNoComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogIncludingPostsAndNotIncludingComments(int id)
        {
            var blog = await _blogService.GetBlogIncludingPostsAndNotIncludingComments(id);

            return Ok(blog);
        }

        [HttpGet]
        [Route("{id:int}/postsAndComments")]
        [Produces(typeof(Blog))]
        public async Task<IActionResult> GetBlogIncludingPostsAndComments(int id)
        {
            var blog = await _blogService.GetBlogIncludingPostsAndComments(id);

            return Ok(blog);
        }
    }
}
