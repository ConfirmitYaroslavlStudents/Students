using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using RenamerLib;

namespace MP3Renamer
{
    public class ProcessExecutor
    {
        public void Execute(Arguments arguments, ILogger logger)
        {
            try
            {
                Processor processor = new Processor(arguments, logger);
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
