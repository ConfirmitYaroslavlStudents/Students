using System;
using Logger;
using RenamerLib.Arguments;

namespace MP3Renamer
{
    public class Program
    {
        public static RenamerArguments GetArguments(string[] args, ILogger logger)
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = null;

            try
            {
                renamerArguments = parser.ParseArguments(args);
            }
            catch (ArgumentException e)
            {
                logger.WriteMessage(e.Message, Status.Error);
                Console.WriteLine("Process failed! See log.txt for full information");
            }

            return renamerArguments;
        }

        public static void Main(string[] args)
        {
            var logger = new TextFileLogger("log.txt");
            var renamerArguments = GetArguments(args, logger);

            ProcessExecutor processExecutor = new ProcessExecutor();
            processExecutor.Execute(renamerArguments, logger);
        }
    }
}
