using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteFileProcessor
    {
        void Execute(IFileInfo slaveFile);
    }
}