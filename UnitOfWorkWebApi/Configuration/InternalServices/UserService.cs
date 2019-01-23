namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using UnitOfWork.Contracts.UnitOfWork;
    using DataModel.Models;

    public class UserService : IUserService
    {
        private readonly ISettings _settings;
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;


        public UserService(ISettings settings, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _settings = settings;
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddUserAsync(User user)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            userRepository.Insert(user);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<User> GetUserAsync(string username)
        {
            var userQueryableRepository = _unitOfWork.GetQueryableRepository<User>();

            return userQueryableRepository
                .GetFirstOrDefaultAsync(
                    predicate: user => user.Username == username,
                    orderBy: null,
                    include: null
                );
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            var queryableUserRepository = _unitOfWork.GetQueryableRepository<User>();

            return queryableUserRepository
                .GetFirstOrDefaultAsync(
                    predicate: user => user.Username == username && user.Password == password,
                    orderBy: null,
                    include: null
                );
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await GetUserAsync(username, password).ConfigureAwait(false);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JwtGeneratorKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(1),

                Issuer = "internal",

                Audience = "internal",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}