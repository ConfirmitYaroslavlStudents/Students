using MasterSlaveSync.Loggers;
using System;

namespace MasterSlaveSync
{
    public class SyncOptions
    {
        internal bool NoDelete { get; private set; } = false;
        internal ILogger Logger { get; private set; } = new SummaryLogger(Console.WriteLine);

        public SyncOptions NoDeleteOn()
        {
            NoDelete = true;
            return this;
        }

        public SyncOptions LogSilent()
        {
            Logger = new SilentLogger();
            return this;
        }

        public SyncOptions LogSummary(Action<string> logListener)
        {
            Logger = new SummaryLogger(logListener);
            return this;
        }

        public SyncOptions LogVerbose(Action<string> logListener)
        {
            Logger = new VerboseLogger(logListener);
            return this;
        }

    }
}
