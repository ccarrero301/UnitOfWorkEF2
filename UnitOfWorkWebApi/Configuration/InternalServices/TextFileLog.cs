namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    using System;
    using Serilog;

    public class TextFileLog : ILog
    {
        public TextFileLog(ISettings settings) => Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(settings.TextFileLogPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        public void LogInfo(string informationDataToLog) => Log.Information(informationDataToLog);

        public void LogException(string exceptionTemplateToLog, Exception exceptionToLog) =>
            Log.Error(exceptionTemplateToLog, exceptionToLog);
    }
}