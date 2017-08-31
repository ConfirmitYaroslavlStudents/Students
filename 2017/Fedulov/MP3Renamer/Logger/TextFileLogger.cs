using System;
using System.IO;

namespace Logger
{
    public class TextFileLogger : ILogger
    {
        private StreamWriter _file;

        public TextFileLogger(string logFileName)
        {
            _file = new StreamWriter(logFileName);   
        }

        public void WriteMessage(String message, Status status)
        {
            WriteStatus(status);
            _file?.WriteLine(message);
        }

        public void WriteStatus(Status status)
        {
            switch (status)
            {
                case Status.Success:
                    _file?.Write("[Success]: ");
                    break;
                case Status.Warning:
                    _file?.Write("[Warning]: ");
                    break;
                case Status.Error:
                    _file?.Write("[Error]: ");
                    break;
                case Status.Info:
                    _file?.Write("[Info]: ");
                    break;
            }
            StoreLogging();   
        }

        public void StoreLogging()
        {
            _file.Flush();
        }
    }
}
