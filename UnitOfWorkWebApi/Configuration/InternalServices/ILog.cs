namespace UnitOfWorkWebApi.Configuration.InternalServices
{
    using System;

    public interface ILog
    {
        void LogInfo(string informationDataToLog);

        void LogException(string exceptionTemplateToLog, Exception exceptionToLog);
    }
}