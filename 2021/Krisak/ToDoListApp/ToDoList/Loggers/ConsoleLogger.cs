using System;
using System.IO;
using ToDoLibrary.Const;

namespace ToDoLibrary.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(Exception e)
        {
            switch (e)
            {
                case WrongEnteredCommandException _:
                case FileNotFoundException _:
                    Console.WriteLine(e.Message);
                    break;

                default:
                    var fileLogger = new FileLogger(Data.LogFileName);
                    fileLogger.Log(e);
                    break; ;
            }
        }
    }
}
