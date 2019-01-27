namespace AppServices.Posts.Contracts
{
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(Post post);
    }
}