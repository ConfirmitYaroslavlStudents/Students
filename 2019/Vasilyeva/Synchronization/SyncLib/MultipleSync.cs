using System.Collections.Generic;
using System.IO;

namespace SyncLib
{
    public class MultipleSync
    {
        private string master;
        private Dictionary<string, bool> slaves = new Dictionary<string, bool>();
        private TextWriter writer;
        private LoggerType loggerType = LoggerType.Summary;
        public MultipleSync(string master)
        {
            this.master = master;
        }

        public MultipleSync SetSlave(string path, bool nodelete = false)
        {
            if (!slaves.ContainsKey(path))
            {
                slaves.Add(path, nodelete);
            }
            else slaves[path] = nodelete;

            return this;
        }

        public MultipleSync SetLoggerType(LoggerType logType, TextWriter writer)
        {
            this.writer = writer;

            loggerType = logType;

            return this;
        }
        public void Synchronize()
        {
            SenchronizeMasterWithSlaves();

            SynchronizeSlavesWithMaster();
        }

        private void SynchronizeSlavesWithMaster()
        {
            foreach (var slave in slaves)
            {
                new Synchronization(master, slave.Key, writer, slave.Value, loggerType).Synchronize();
            }
        }

        private void SenchronizeMasterWithSlaves()
        {
            foreach (var slave in slaves)
            {
                new Synchronization(master, slave.Key, writer, slave.Value, loggerType).Synchronize();
            }
        }
    }
}
