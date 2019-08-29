using MasterSlaveSync.Loggers;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizator
    {
        public Synchronizator(string masterPath, string slavePath, SyncOptions options, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;

            if (!DirectoryValidator.MasterExists(masterPath, FileSystem))
            {
                throw new ArgumentException("Master directory does not exist");
            }
            MasterPath = masterPath;
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
        internal IMessageCreator Logger { get; private set; }

        public void Run()
        {
            var collector = new ConflictsCollector();
            IDirectoryInfo master = FileSystem.DirectoryInfo.FromDirectoryName(MasterPath);

            foreach (var slavePath in SlavePaths)
            {
                IDirectoryInfo slave = FileSystem.DirectoryInfo.FromDirectoryName(slavePath);

                Resolver.ResolveConflicts(collector.CollectConflicts(master, slave), MasterPath, slavePath);
            }

        }

        public void AddSlave(string slavePath)
        {
            SlavePaths.Add(slavePath);
            if (!DirectoryValidator.DirectoriesDoNotContainEachOther(MasterPath, slavePath, FileSystem))
            {
                throw new ArgumentException("Directories should not contain each other");
            }

            FileSystem.Directory.CreateDirectory(slavePath);
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
