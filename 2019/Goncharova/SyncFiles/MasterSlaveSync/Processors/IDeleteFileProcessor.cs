using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteFileProcessor
    {
        bool Execute(IFileInfo slaveFile);
    }
}