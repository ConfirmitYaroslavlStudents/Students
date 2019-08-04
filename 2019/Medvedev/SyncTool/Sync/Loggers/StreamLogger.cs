using System.IO;
using Sync.Resolutions;

namespace Sync.Loggers
{
    public class StreamLogger : Logger
    {
        private TextWriter _writer;

        public StreamLogger(TextWriter writer, LoggerOption option) 
            : base(option)
        {
            _writer = writer;
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

            _writer.WriteLine(log);
        }
    }
}