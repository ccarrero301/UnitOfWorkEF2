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

        public string TextFileLogPath => GetValueFormConfiguration("TextFileLog", "Path");

        private string GetValueFormConfiguration(string root, string key) => _configuration[$"{root}:{key}"];
    }
}
