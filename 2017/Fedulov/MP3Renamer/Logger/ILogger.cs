using System;

namespace Logger
{
    public enum Status
    {
        Info,
        Success,
        Warning,
        Error
    }

    public interface ILogger
    {
        void WriteMessage(String message, Status status);
        void WriteStatus(Status status);
        void StoreLogging();
    }
}
