using System;
using Logger;
using RenamerLib;
using RenamerLib.Arguments;

namespace MP3Renamer
{
    public class ProcessExecutor
    {
        public void Execute(RenamerArguments renamerArguments, ILogger logger)
        {
            try
            {
                Processor processor = new Processor(renamerArguments, logger);
                processor.Process();
            }
            catch (Exception e)
            {
                logger.WriteMessage(e.Message, Status.Error);
                Console.WriteLine("Process failed! See log.txt for full information");
            }
        }
    }
}
