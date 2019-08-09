using MasterSlaveSync.Conflict;
using System;
using System.Collections.Generic;

namespace MasterSlaveSync
{
    public class SyncProcessor
    {
        #region privates
        private readonly string masterPath;
        private readonly string slavePath;

        private IDeleteFileProcessor deleteFileProcessor = new DefaultDeleteFileProcessor();
        private ICopyFileProcessor copyFileProcessor = new DefaultCopyFileProcessor();
        private IUpdateFileProcessor updateFileProcessor = new DefaultUpdateFileProcessor();

        private IDeleteDirectoryProcessor deleteDirectoryProcessor = new DefaultDeleteDirectoryProcessor();
        private ICopyDirectoryProcessor copyDirectoryProcessor = new DefaultCopyDirectoryProcessor();

        private ILogger logger;
        private Action<string> logListener;

        #endregion

        public SyncProcessor(SyncOptions syncOptions, string masterPath, string slavePath, 
            LogLevels level, Action<string> logListener)
        {
            this.masterPath = masterPath;
            this.slavePath = slavePath;

            if(syncOptions.NoDelete == true)
            {
                deleteFileProcessor = new NoDeleteFileProcessor();
                deleteDirectoryProcessor = new NoDeleteDirectoryProcessor();
            }

            SetLogLevel(level);
            this.logListener = logListener;

        }

        public void SetLogLevel(LogLevels level)
        {
            switch (level)
            {
                case (LogLevels.Silent):
                    logger = new SilentLogger();
                    break;
                case (LogLevels.Summary):
                    logger = new SummaryLogger();
                    break;
                case (LogLevels.Verbose):
                    logger = new VerboseLogger();
                    break;
            }
        }

        public void ResolveConflicts(List<IConflict> conflicts)
        {
            foreach(var conflict in conflicts)
            {
                if(conflict.GetType() == typeof(FileConflict))
                {
                    FileConflict fileConflict = (FileConflict)conflict;

                    if(fileConflict.MasterFile == null)
                    {
                        ResolveByDeletion(fileConflict);
                    }
                    else if (fileConflict.SlaveFile == null)
                    {
                        ResolveByCopy(fileConflict);
                    }
                    else
                    {
                        ResolveByUpdate(fileConflict);
                    }
                }

                if (conflict.GetType() == typeof(DirectoryConflict))
                {
                    DirectoryConflict directoryConflict = (DirectoryConflict)conflict;

                    if (directoryConflict.MasterDirectory == null)
                    {
                        ResolveByDeletion(directoryConflict);
                    }
                    else
                    {
                        ResolveByCopy(directoryConflict);

                    }
                }
            }
        }

        private void ResolveByCopy(DirectoryConflict directoryConflict)
        {
            if (copyDirectoryProcessor.Execute(directoryConflict.MasterDirectory,
                masterPath, slavePath))
            {
                logListener(logger.LogDirectoryCopy(directoryConflict.MasterDirectory));
            }
        }

        private void ResolveByDeletion(DirectoryConflict directoryConflict)
        {
            if (deleteDirectoryProcessor.Execute(directoryConflict.SlaveDirectory))
            {
                logListener(logger.LogDirectoryDeletion(directoryConflict.SlaveDirectory));
            }
        }

        private void ResolveByUpdate(FileConflict fileConflict)
        {
            if (updateFileProcessor.Execute(fileConflict))
            {
                logListener(logger.LogFileUpdate(fileConflict));
            }
        }

        private void ResolveByCopy(FileConflict fileConflict)
        {
            if (copyFileProcessor.Execute(fileConflict.MasterFile, masterPath, slavePath))
            {
                logListener(logger.LogFileCopy(fileConflict.MasterFile));
            }
        }

        private void ResolveByDeletion(FileConflict fileConflict)
        {
            if (deleteFileProcessor.Execute(fileConflict.SlaveFile))
            {
                logListener(logger.LogFileDeletion(fileConflict.SlaveFile));
            }
        }
    }
}
