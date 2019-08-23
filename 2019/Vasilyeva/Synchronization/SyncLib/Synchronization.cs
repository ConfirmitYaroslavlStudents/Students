namespace SyncLib
{
    public class Synchronization
    {
        public Synchronization(string masterPath, string slavePath, bool noDelete = false, EnumLog enumLog = EnumLog.Silent)
        {
            master = masterPath;
            slave = slavePath;
            noDeleteOption = noDelete;
            loggerType = enumLog;
        }

        private readonly string master;
        private readonly string slave;
        private EnumLog loggerType;
        private bool noDeleteOption;

        private BaseSeeker conflictSeeker;

        public void Synchronize()
        {
            if (noDeleteOption)
            {
                conflictSeeker = new DefaultConflictSeeker(master, slave);
            }
            else
            {
                conflictSeeker = new RemoveConflictSeeker(master, slave);
            }

            conflictSeeker.GetMasterConflict().ForEach(x => new Log(x.Resolve(), loggerType).Create());

            conflictSeeker.GetSlaveConflicts().ForEach(x => new Log(x.Resolve(), loggerType).Create());

        }

        public Synchronization SetNoDeleteOption(bool option)
        {
            noDeleteOption = option;

            return this;
        }

        public Synchronization SetLogOption(EnumLog option)
        {
            loggerType = option;

            return this;
        }
    }
}
