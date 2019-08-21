using System.Collections.Generic;

namespace GeneralizeSynchLibrary
{
    public class ArgParser
    {
        public List<string> FileNames = new List<string>();

        public bool NoDelete = false;

        public LoggerConstant Mode = LoggerConstant.Silent;
        public LoggerConstant GetLoggerConstant(string input)
        {
            if (input == "--silent")
                return LoggerConstant.Silent;
            if (input == "--summary")
                return LoggerConstant.Summary;
            if (input == "--verbose")
                return LoggerConstant.Verbose;
            return LoggerConstant.None;
        }

        public ArgParser(string[] arg)
        {
            for(int i = 0; i < arg.Length; i++)
            {
                if (GetLoggerConstant(arg[i]) != LoggerConstant.None)
                    Mode = GetLoggerConstant(arg[i]);
                else if (arg[i] == "--no-delete")
                    NoDelete = true;
                else
                    FileNames.Add(arg[i]);
            }
        }
    }
}
