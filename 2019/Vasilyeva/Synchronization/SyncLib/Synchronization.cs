namespace SyncLib
{
    public class Synchronization
    {
        public Synchronization(string masterPath, string slavePath, bool noDelete = true, EnumLog enumLog = EnumLog.Silent)
        {
            master = masterPath;
            slave = slavePath;
            noDeleteOption = noDelete;
            logger = enumLog;
        }

        private readonly string master;
        private readonly string slave;
        private EnumLog logger;
        private bool noDeleteOption;

        private BaseSeeker seeker;

        public void Synchronaze()
        {
            if (noDeleteOption)
            {
                seeker = new DefaultConflictSeeker(master, slave);
            }
            else
            {
                seeker = new RemoveConflictSeeker(master, slave);
            }

            seeker.GetMasterConflict().ForEach(x => new Log(x.Resolve(), logger).Create());

            seeker.GetSlaveConflicts().ForEach(x => new Log(x.Resolve(), logger).Create());

        }

        public Synchronization SetNoDeleteOption(bool option)
        {
            noDeleteOption = option;

            return this;
        }

        public Synchronization SetLogOption(EnumLog option)
        {
            logger = option;

            return this;
        }
    }
}
