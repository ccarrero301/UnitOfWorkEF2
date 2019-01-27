namespace Data.Users.Services
{
    using System;
    using System.Text;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using UnitOfWork.Contracts.UnitOfWork;
    using DomainUsers = Domain.Users;
    using Shared.Settings;
    using Specifications;
    using Contracts;
    using AutoMapper;
    
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ISettings _settings;
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public UserService(IMapper mapper, ISettings settings, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _mapper = mapper;
            _settings = settings;
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddUserAsync(DomainUsers.User domainUser)
        {
            var dataUser = _mapper.Map<User>(domainUser);

            var userRepository = _unitOfWork.GetRepository<User>();

            userRepository.Insert(dataUser);

            return _unitOfWork.SaveChangesAsync();
        }

        public async Task<DomainUsers.User> GetUserAsync(string username)
        {
            var userQueryableRepository = _unitOfWork.GetQueryableRepository<User>();

            var dataUser = await userQueryableRepository.GetFirstOrDefaultAsync(new UserByNameSpecification(username));

            var domainUser = _mapper.Map<DomainUsers.User>(dataUser);

            return domainUser;
        }

        public async Task<DomainUsers.User> GetUserAsync(string username, string password)
        {
            var queryableUserRepository = _unitOfWork.GetQueryableRepository<User>();

            var dataUser = await queryableUserRepository.GetFirstOrDefaultAsync(new UserByCredentialsSpecification(username, password));

            var domainUser = _mapper.Map<DomainUsers.User>(dataUser);

            return domainUser;
        }

        public async Task<DomainUsers.User> AuthenticateUserAsync(DomainUsers.User user)
        {
            var authenticatedUser = await GetUserAsync(user.Username, user.Password).ConfigureAwait(false);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JwtGeneratorKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, authenticatedUser.Role)
                }),

                Expires = DateTime.UtcNow.AddDays(1),

                Issuer = "blogs.api",
                Audience = "blogs.api",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            authenticatedUser.Token = tokenHandler.WriteToken(token);

            authenticatedUser.Password = null;

            return authenticatedUser;
        }
    }
}