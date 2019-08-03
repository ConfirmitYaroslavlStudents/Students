using System;
using Sync.Resolutions;

namespace Sync.Loggers
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LoggerOption option) 
            : base(option)
        {
        }

        public override void Log(IResolution resolution)
        {
            if (Option == LoggerOption.Silent)
                return;

            string log = "";
            if (resolution is UpdateResolution ur)
                log = UpdateLog(ur);
            if (resolution is CopyResolution cr)
                log = CopyLog(cr);
            if (resolution is DeleteResolution dr)
                log = DeleteLog(dr);

            Console.WriteLine(log);
        }
    }
}