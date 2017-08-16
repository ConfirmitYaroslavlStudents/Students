using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Logger;
using RenamerLib;

namespace MP3Renamer
{
    public class Program
    {
        public static Arguments GetArguments(string[] args, ILogger logger)
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = null;

            try
            {
                arguments = parser.ParseArguments(args);
            }
            catch (ArgumentException e)
            {
                logger.WriteMessage(e.Message, Status.Error);
                Console.WriteLine("Process failed! See log.txt for full information");
            }

            return arguments;
        }

        public static void Main(string[] args)
        {
            var logger = new TextFileLogger("log.txt");
            var arguments = GetArguments(args, logger);

            ProcessExecutor processExecutor = new ProcessExecutor();
            processExecutor.Execute(arguments, logger);
        }
    }
}
