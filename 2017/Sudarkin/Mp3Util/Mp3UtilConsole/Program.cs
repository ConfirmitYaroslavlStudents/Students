using System;
using Mp3UtilLib;
using Mp3UtilLib.Arguments;
using Mp3UtilLib.Logger;

namespace Mp3UtilConsole
{
    internal class Program
    {
        private static readonly ConsoleLogger Logger = new ConsoleLogger();

        private static void Main(string[] args)
        {
            Args arguments = null;
            try
            {
                arguments = ArgumentsParser.Parse(args);
            }
            catch (ArgumentException ex)
            {
                Logger.Write(ex.Message, LogStatus.Error);
                return;
            }

            Processor processor = new Processor(arguments, Logger);
            processor.Execute();
        }
    }
}