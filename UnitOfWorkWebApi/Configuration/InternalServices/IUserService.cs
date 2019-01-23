namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    using System.Threading.Tasks;
    using DataModel.Models;

    public interface IUserService
    {
        Task<int> AddUserAsync(User user);

        Task<User> GetUserAsync(string username);

        Task<User> GetUserAsync(string username, string password);

        Task<User> AuthenticateUserAsync(string username, string password);
    }
}