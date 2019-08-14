using System.IO.Abstractions;
using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal class SummaryLogger : ILogger
    {
        public string LogDirectoryCopy(IDirectoryInfo masterDirectory)
        {
            return $"Copied {masterDirectory.Name} directory";
        }

        public string LogDirectoryDeletion(IDirectoryInfo slaveDirectory)
        {
            return $"Deleted {slaveDirectory.Name} directory";
        }

        public string LogFileCopy(IFileInfo masterFile)
        {
            return $"Copied {masterFile.Name} file";
        }

        public string LogFileDeletion(IFileInfo slaveFile)
        {
            return $"Deleted {slaveFile.Name} file";
        }

        public string LogFileUpdate(FileConflict fileConflict)
        {
            return $"Updated {fileConflict.SlaveFile.Name} file";
        }
    }
}