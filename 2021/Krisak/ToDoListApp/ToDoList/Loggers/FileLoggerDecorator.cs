using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Loggers
{
    public class FileLoggerDecorator : BaseLoggerDecorator
    {
        private string _fileName;
        public FileLoggerDecorator (ILogger logger, string fileName) : base(logger) 
        {
            _fileName = fileName;
        }
        public override void Log(string message)
        {
            base.Log(message);
            File.AppendAllText(_fileName, message);
            File.AppendAllText(_fileName, "\n");
        }
    }
}
