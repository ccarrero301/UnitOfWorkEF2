namespace Data.Users.Contracts
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<int> AddUserAsync(User user);

        Task<User> GetUserAsync(string username);

        Task<User> GetUserAsync(string username, string password);

        Task<User> AuthenticateUserAsync(User user);
    }
}