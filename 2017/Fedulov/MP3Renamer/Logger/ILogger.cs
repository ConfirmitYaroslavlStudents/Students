using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void StopLogging();
    }
}
