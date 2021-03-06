﻿namespace AppServices.Posts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataPostsContracts = Data.Posts.Contracts;
    using DomainPosts = Domain.Posts;
    using Shared.DTOs;
    using Contracts;
    using AutoMapper;

    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly DataPostsContracts.IPostService _postService;

        public PostService(IMapper mapper, DataPostsContracts.IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        public Task<int> AddPostToBlogAsync(Post dtoPost)
        {
            var domainPost = _mapper.Map<DomainPosts.Post>(dtoPost);

            return _postService.AddPostToBlogAsync(domainPost);
        }

        public async Task<IEnumerable<Post>> GetPostsFromBlogContainingWordAsync(int blogId, string word)
        {
            var domainPosts =
                await _postService.GetPostsFromBlogContainingWordAsync(blogId, word).ConfigureAwait(false);

            var dtoPosts = _mapper.Map<IEnumerable<Post>>(domainPosts);

            return dtoPosts;
        }
    }
}