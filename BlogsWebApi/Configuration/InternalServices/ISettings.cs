namespace BlogsWebApi.Configuration.InternalServices
{
    public interface ISettings
    {
        string UnitOfWorkConnectionString { get; }

        string JwtGeneratorKey { get; }

        string JwtIssuer { get; }

        string JwtAudience { get; }

        string B2BSecret { get; }

        string TextFileLogPath { get; }
    }
}