namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    public interface ISettings
    {
        string UnitOfWorkConnectionString { get; }

        string TextFileLogPath { get; }
    }
}
