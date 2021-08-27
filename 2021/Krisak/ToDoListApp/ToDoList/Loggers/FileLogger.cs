using System;
using System.IO;

namespace ToDoLibrary.Loggers
{
    public class FileLogger:ILogger
    {
        private string _fileName;

        public FileLogger( string fileName) 
            => _fileName = fileName;

        public void Log(Exception e)
        {
            File.AppendAllText(_fileName, e.Message);
            File.AppendAllText(_fileName, "\n");
        }
    }
}
