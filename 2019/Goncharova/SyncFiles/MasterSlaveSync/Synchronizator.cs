using MasterSlaveSync.Loggers;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizator
    {
        private readonly IDirectoryInfo master;
        private readonly List<IDirectoryInfo> slaves;

        public Synchronizator(string masterPath, string slavePath, SyncOptions options, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;

            MasterPath = masterPath;
            master = FileSystem.DirectoryInfo.FromDirectoryName(masterPath);

            slaves = new List<IDirectoryInfo>();
            AddSlave(slavePath);

            ConfigureResolver(options);
            ConfigureLogger(options);
        }

        public Synchronizator(string masterPath, string slavePath, SyncOptions options)
            : this(masterPath, slavePath, options, new FileSystem()) { }

        public string MasterPath { get; }
        public List<string> SlavePaths { get; } = new List<string>();
        internal IFileSystem FileSystem { get; }
        internal IResolver Resolver { get; private set; }
        internal ILogger Logger { get; private set; }

        public void Run()
        {
            var collector = new ConflictsCollector();

            foreach (var slave in slaves)
            {
                Resolver.ResolveConflicts(collector.CollectConflicts(master, slave), MasterPath, slave.FullName);
            }

        }

        public void AddSlave(string slavePath)
        {
            SlavePaths.Add(slavePath);
            slaves.Add(FileSystem.DirectoryInfo.FromDirectoryName(slavePath));
        }

        private void ConfigureResolver(SyncOptions options)
        {
            Resolver = new Resolver();

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
