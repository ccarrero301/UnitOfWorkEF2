namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    using Microsoft.Extensions.Configuration;

    public class ApplicationSettings : ISettings
    {
        private readonly IConfiguration _configuration;

        public ApplicationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string UnitOfWorkConnectionString => GetValueFormConfiguration("UnitOfWork", "ConnectionString");

        public string JwtGeneratorKey => GetValueFormConfiguration("Jwt", "GeneratorKey");

        public string JwtIssuer => GetValueFormConfiguration("Jwt", "Issuer");

        public string JwtAudience => GetValueFormConfiguration("Jwt", "Audience");
        public string B2BSecret => GetValueFormConfiguration("B2B", "Secret");

        public string TextFileLogPath => GetValueFormConfiguration("TextFileLog", "Path");

        private string GetValueFormConfiguration(string root, string key) => _configuration[$"{root}:{key}"];
    }
}