using MasterSlaveSync.Conflict;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class VerboseLogger : ILogger
    {
        public string LogDirectoryCopy(IDirectoryInfo masterDirectory)
        {
            return $"Copied {masterDirectory.Name} directory from " +
                $"{masterDirectory.FullName.Substring(masterDirectory.FullName.Length - masterDirectory.Name.Length)}";
        }

        public string LogDirectoryDeletion(IDirectoryInfo slaveDirectory)
        {
            return $"Deleted {slaveDirectory.Name} directory from " +
                $"{slaveDirectory.FullName.Substring(slaveDirectory.FullName.Length - slaveDirectory.Name.Length)}";
        }

        public string LogFileCopy(IFileInfo masterFile)
        {
            return $"Copied {masterFile.Name} file from " + $"{masterFile.DirectoryName}";
        }

        public string LogFileDeletion(IFileInfo slaveFile)
        {
            return $"Deleted {slaveFile.Name} file from " + $"{slaveFile.DirectoryName}";
        }

        public string LogFileUpdate(FileConflict fileConflict)
        {
            return $"Updated {fileConflict.SlaveFile.Name} file";
        }
    }
}