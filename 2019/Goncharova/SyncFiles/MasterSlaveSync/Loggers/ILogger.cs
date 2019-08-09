using System.IO.Abstractions;
using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal interface ILogger
    {
        string LogFileDeletion(IFileInfo slaveFile);
        string LogFileCopy(IFileInfo masterFile);
        string LogFileUpdate(FileConflict fileConflict);
        string LogDirectoryDeletion(IDirectoryInfo slaveDirectory);
        string LogDirectoryCopy(IDirectoryInfo masterDirectory);
    }
}