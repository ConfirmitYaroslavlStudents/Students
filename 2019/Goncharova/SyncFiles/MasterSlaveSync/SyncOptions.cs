using MasterSlaveSync.Loggers;
using System;

namespace MasterSlaveSync
{
    public class SyncOptions
    {
        internal bool NoDelete { get; private set; } = false;
        internal IMessageCreator Logger { get; private set; } = new SummaryMessageCreator(Console.WriteLine);

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
            Logger = new SummaryMessageCreator(logListener);
            return this;
        }

        public SyncOptions LogVerbose(Action<string> logListener)
        {
            Logger = new VerboseMessageCreator(logListener);
            return this;
        }

    }
}
