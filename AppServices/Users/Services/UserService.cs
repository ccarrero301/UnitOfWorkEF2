namespace AppServices.Users.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Shared.DTOs;
    using DataUsersContracts = Data.Users.Contracts;
    using DomainUser = Domain.Users;
    using Contracts;

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataUsersContracts.IUserService _userService;

        public UserService(IMapper mapper, DataUsersContracts.IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public Task<int> AddUserAsync(User dtoUser)
        {
            var domainUser = _mapper.Map<DomainUser.User>(dtoUser);

            return _userService.AddUserAsync(domainUser);
        }

        public async Task<User> GetUserAsync(string username)
        {
            var domainUser = await _userService.GetUserAsync(username).ConfigureAwait(false);

            var dtoUser = _mapper.Map<User>(domainUser);

            return dtoUser;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var domainUser = await _userService.GetUserAsync(username, password).ConfigureAwait(false);

            var dtoUser = _mapper.Map<User>(domainUser);

            return dtoUser;
        }

        public async Task<User> AuthenticateUserAsync(User dtoUser)
        {
            var domainUser = _mapper.Map<DomainUser.User>(dtoUser);

            var domainAuthenticatedUser = await _userService.AuthenticateUserAsync(domainUser).ConfigureAwait(false);

            var dtoAuthenticatedUser = _mapper.Map<User>(domainAuthenticatedUser);

            return dtoAuthenticatedUser;
        }
    }
}