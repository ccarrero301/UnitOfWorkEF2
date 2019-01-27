namespace Data.Users.Contracts
{
    using System.Threading.Tasks;
    using DomainUsers = Domain.Users;

    public interface IUserService
    {
        Task<int> AddUserAsync(DomainUsers.User domainUser);

        Task<DomainUsers.User> GetUserAsync(string username);

        Task<DomainUsers.User> GetUserAsync(string username, string password);

        Task<DomainUsers.User> AuthenticateUserAsync(DomainUsers.User domainUser);
    }
}