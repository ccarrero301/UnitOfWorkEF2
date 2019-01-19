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
        [Produces(typeof(IEnumerable<Blog>))]
        public async Task<IActionResult> Get()
        {
            var blogs = await _blogService.GetAllBlogs();

            return Ok(blogs.ToList());
        }
    }
}
