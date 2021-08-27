using System;
using System.IO;
using ToDoLibrary.Const;

namespace ToDoLibrary.Loggers
{
    public class WebLogger : ILogger
    {
        public Exception @Exception { get; private set; }

        public void Log(Exception e)
        {
            @Exception = e;

            switch (e)
            {
                case WrongEnteredCommandException _:
                case FileNotFoundException _:
                    return;

                default:
                    var fileLogger = new FileLogger(Data.LogFileName);
                    fileLogger.Log(e);
                    break;
            }
        }
    }
}