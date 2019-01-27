namespace AppServices.Users.Contracts
{
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface IUserService
    {
        Task<int> AddUserAsync(User dtoUser);

        Task<User> GetUserAsync(string username);

        Task<User> GetUserAsync(string username, string password);

        Task<User> AuthenticateUserAsync(User dtoUser);
    }
}