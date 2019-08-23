using System.Collections.Generic;

namespace SyncLib
{
    public class MultipleSync
    {
        private string master;
        private Dictionary<string, bool> slaves = new Dictionary<string, bool>();
        private EnumLog loggerType = EnumLog.Summary;
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

        public MultipleSync SetLoggerType(EnumLog logType)
        {
            loggerType = logType;

            return this;
        }
        public void Synchronize()
        {
            foreach (var slave in slaves)
                new Synchronization(master, slave.Key, slave.Value, loggerType).Synchronaze();

            foreach (var slave in slaves)
                new Synchronization(master, slave.Key, slave.Value, loggerType).Synchronaze();
        }
    }
}
