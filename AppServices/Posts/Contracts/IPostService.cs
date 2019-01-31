namespace AppServices.Posts.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(Post post);

        Task<IEnumerable<Post>> GetPostsFromBlogContainingWordAsync(int blogId, string word);
    }
}