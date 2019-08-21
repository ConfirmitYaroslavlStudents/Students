using MasterSlaveSync.Loggers;
using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizator
    {
        private readonly IDirectoryInfo master;
        private readonly IDirectoryInfo slave;

        public Synchronizator(string masterPath, string slavePath, SyncOptions options, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;

            master = FileSystem.DirectoryInfo.FromDirectoryName(masterPath);
            slave = FileSystem.DirectoryInfo.FromDirectoryName(slavePath);

            ConfigureResolver(masterPath, slavePath, options);
            ConfigureLogger(options);
        }

        public Synchronizator(string masterPath, string slavePath, SyncOptions options)
            : this(masterPath, slavePath, options, new FileSystem()) { }

        internal IFileSystem FileSystem { get; }
        internal IResolver Resolver { get; private set; }
        internal ILogger Logger { get; private set; }

        public void Run()
        {
            var collector = new ConflictsCollector();

            Resolver.ResolveConflicts(collector.CollectConflicts(master, slave));
        }

        private void ConfigureResolver(string masterPath, string slavePath, SyncOptions options)
        {
            Resolver = new Resolver(masterPath, slavePath);

            if (options.NoDelete)
            {
                Resolver.DeleteFileProcessor = new NoDeleteFileProcessor();
                Resolver.DeleteDirectoryProcessor = new NoDeleteDirectoryProcessor();
            }
        }
        private void ConfigureLogger(SyncOptions options)
        {
            Logger = options.Logger;
            SetLogEvents();
        }

        private void SetLogEvents()
        {
            Resolver.DeleteFileProcessor.FileDeleted += Logger.LogFileDeletion;
            Resolver.CopyFileProcessor.FileCopied += Logger.LogFileCopy;
            Resolver.UpdateFileProcessor.FileUpdated += Logger.LogFileUpdate;
            Resolver.DeleteDirectoryProcessor.DirectoryDeleted += Logger.LogDirectoryDeletion;
            Resolver.CopyDirectoryProcessor.DirectoryCopied += Logger.LogDirectoryCopy;
        }
    }
}
