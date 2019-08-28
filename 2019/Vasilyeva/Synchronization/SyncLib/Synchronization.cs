using SyncLib.Loggers;
using SyncLib.Seeker;
using System.IO;

namespace SyncLib
{
    public class Synchronization
    {
        public Synchronization(string masterPath, string slavePath, TextWriter writer, bool noDelete = false, LoggerType enumLog = LoggerType.Summary)
        {
            master = masterPath;
            slave = slavePath;
            noDeleteOption = noDelete;
            logger = LoggerFactory.GetLogger(enumLog, writer);
            conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(noDeleteOption, master, slave);
        }

        private readonly string master;
        private readonly string slave;
        private readonly bool noDeleteOption;

        private BaseSeeker conflictSeeker;
        private readonly ILogger logger;

        public void Synchronize()
        {
            conflictSeeker.GetMasterConflict().ForEach(x => {
                x.Accept(new ResolverVisitor());
                x.Accept(logger);
                });

            conflictSeeker.GetSlaveConflicts().ForEach(x => {
                x.Accept(new ResolverVisitor());
                x.Accept(logger);
            });

            logger.Log();
        }
    }
}
