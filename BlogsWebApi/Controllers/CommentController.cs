﻿namespace BlogsWebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AppServices.Comments.Contracts;
    using Shared.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("Add")]
        [Produces(typeof(int))]
        public async Task<IActionResult> AddCommentToPost(Comment comment)
        {
            var numberOfRecords = await _commentService.AddCommentToPostAsync(comment).ConfigureAwait(false);

            return Ok(numberOfRecords);
        }
    }
}