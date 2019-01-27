namespace Data.Posts.Contracts
{
    using System.Threading.Tasks;
    using DomainPosts = Domain.Posts;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(DomainPosts.Post domainPost);
    }
}