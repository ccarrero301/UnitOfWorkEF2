namespace Data.Posts.Contracts
{
    using System.Threading.Tasks;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(Post post);
    }
}