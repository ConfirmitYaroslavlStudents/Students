using System;
using System.IO.Abstractions;
using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal class SilentLogger : ILogger
    {
        public SilentLogger()
        {
        }

        public string LogDirectoryCopy(IDirectoryInfo masterDirectory)
        {
            return String.Empty;
        }

        public string LogDirectoryDeletion(IDirectoryInfo slaveDirectory)
        {
            return String.Empty;
        }

        public string LogFileCopy(IFileInfo masterFile)
        {
            return String.Empty;
        }

        public string LogFileDeletion(IFileInfo slaveFile)
        {
            return String.Empty;
        }

        public string LogFileUpdate(FileConflict fileConflict)
        {
            return String.Empty;
        }
    }
}