using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizer
    {
        private readonly IDirectoryInfo master;
        private readonly IDirectoryInfo slave;

        public Synchronizer(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;

            master = FileSystem.DirectoryInfo.FromDirectoryName(masterPath);
            slave = FileSystem.DirectoryInfo.FromDirectoryName(slavePath);

            Resolver = new Resolver(masterPath, slavePath);
        }

        public Synchronizer(string masterPath, string slavePath)
            : this(masterPath, slavePath, new FileSystem()) { }

        internal IFileSystem FileSystem { get; set; }

        internal Resolver Resolver { get; private set; }
        internal ILogger Logger { get; set; }

        public static SynchronizerBuilder Sync(string masterPath, string slavePath)
        {
            return new SynchronizerBuilder(masterPath, slavePath);
        }
        public static SynchronizerBuilder SyncWithMock(string masterPath, string slavePath, IFileSystem mockFileSystem)
        {
            return new SynchronizerBuilder(masterPath, slavePath, mockFileSystem);
        }

        public void Run()
        {
            var collector = new ConflictsCollector();

            Resolver.ResolveConflicts(collector.CollectConflicts(master, slave));
        }
    }
}
