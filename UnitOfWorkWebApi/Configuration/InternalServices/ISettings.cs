namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    public interface ISettings
    {
        string UnitOfWorkConnectionString { get; }

        string JwtGeneratorKey { get; }

        string TextFileLogPath { get; }
    }
}