using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class TextFileLogger : ILogger
    {
        private System.IO.StreamWriter file;

        public TextFileLogger(string logFileName)
        {
            file = new StreamWriter(logFileName);   
        }

        public void WriteMessage(String message, Status status)
        {
            WriteStatus(status);
            file?.WriteLine(message);
        }

        public void WriteStatus(Status status)
        {
            switch (status)
            {
                case Status.Success:
                    file?.Write("[Success]: ");
                    break;
                case Status.Warning:
                    file?.Write("[Warning]: ");
                    break;
                case Status.Error:
                    file?.Write("[Error]: ");
                    break;
                case Status.Info:
                    file?.Write("[Info]: ");
                    break;
            }   
        }

        public void StopLogging()
        {
            file.Close();
        }
    }
}
