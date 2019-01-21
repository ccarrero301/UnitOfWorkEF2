namespace Services
{
    using System.Threading.Tasks;
    using DataModel.Models;

    public interface IPostService
    {
        Task<int> AddPostToBlogAsync(Post post);
    }
}