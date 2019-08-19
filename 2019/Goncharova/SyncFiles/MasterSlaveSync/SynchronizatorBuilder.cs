using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class SynchronizatorBuilder
    {
        private readonly Synchronizator synchronizer;

        public SynchronizatorBuilder(string masterPath, string slavePath)
        {
            synchronizer = new Synchronizator(masterPath, slavePath);
        }
        public SynchronizatorBuilder(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            synchronizer = new Synchronizator(masterPath, slavePath, fileSystem);
        }

        public SynchronizatorBuilder NoDelete()
        {
            synchronizer.Resolver.DeleteFileProcessor = new NoDeleteFileProcessor();
            synchronizer.Resolver.DeleteDirectoryProcessor = new NoDeleteDirectoryProcessor();

            return this;
        }

        public SynchronizatorBuilder LogSummary(Action<string> logListener)
        {
            synchronizer.Logger = new SummaryLogger(logListener);
            SetLogEvents();

            return this;
        }

        public SynchronizatorBuilder LogVerbose(Action<string> logListener)
        {
            synchronizer.Logger = new VerboseLogger(logListener);
            SetLogEvents();

            return this;
        }

        public void Run() => synchronizer.Run();


        private void SetLogEvents()
        {
            synchronizer.Resolver.DeleteFileProcessor.FileDeleted += synchronizer.Logger.LogFileDeletion;
            synchronizer.Resolver.CopyFileProcessor.FileCopied += synchronizer.Logger.LogFileCopy;
            synchronizer.Resolver.UpdateFileProcessor.FileUpdated += synchronizer.Logger.LogFileUpdate;
            synchronizer.Resolver.DeleteDirectoryProcessor.DirectoryDeleted += synchronizer.Logger.LogDirectoryDeletion;
            synchronizer.Resolver.CopyDirectoryProcessor.DirectoryCopied += synchronizer.Logger.LogDirectoryCopy;
        }

    }
}
