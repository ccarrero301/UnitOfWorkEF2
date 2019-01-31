namespace Data.Posts.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DomainPosts = Domain.Posts;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(DomainPosts.Post domainPost);

        Task<IEnumerable<DomainPosts.Post>> GetPostsFromBlogContainingWordAsync(int blogId, string word);
    }
}