namespace BlogsWebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AppServices.Posts.Contracts;
    using Shared.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("Add")]
        [Produces(typeof(int))]
        public async Task<IActionResult> AddPostToBlog(Post post)
        {
            var numberOfRecords = await _postService.AddPostToBlogAsync(post).ConfigureAwait(false);

            return Ok(numberOfRecords);
        }
    }
}