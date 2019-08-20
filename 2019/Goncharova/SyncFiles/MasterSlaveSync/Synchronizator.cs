using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizator
    {
        private readonly IDirectoryInfo master;
        private readonly IDirectoryInfo slave;

        public Synchronizator(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;

            master = FileSystem.DirectoryInfo.FromDirectoryName(masterPath);
            slave = FileSystem.DirectoryInfo.FromDirectoryName(slavePath);

            Resolver = new Resolver(masterPath, slavePath);
        }

        public Synchronizator(string masterPath, string slavePath)
            : this(masterPath, slavePath, new FileSystem()) { }

        internal IFileSystem FileSystem { get; set; }
        internal Resolver Resolver { get; private set; }
        internal ILogger Logger { get; set; }

        public static SynchronizatorBuilder Sync(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            return new SynchronizatorBuilder(masterPath, slavePath, fileSystem);
        }

        public void Run()
        {
            var collector = new ConflictsCollector();

            Resolver.ResolveConflicts(collector.CollectConflicts(master, slave));
        }
    }
}
