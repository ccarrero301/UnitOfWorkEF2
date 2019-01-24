﻿namespace UnitOfWorkWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Configuration.InternalServices;
    using DataModel.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Add")]
        [Produces(typeof(int))]
        public async Task<IActionResult> AddBlog(User user)
        {
            var numberOfRecords = await _userService.AddUserAsync(user).ConfigureAwait(false);

            return Ok(numberOfRecords);
        }

        [HttpGet]
        [Route("{username}")]
        [Produces(typeof(User))]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserAsync(username).ConfigureAwait(false);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(User user)
        {
            var authenticatedUser = await _userService.AuthenticateUserAsync(user.Username, user.Password).ConfigureAwait(false);

            return Ok(authenticatedUser);
        }
    }
}