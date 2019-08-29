using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface IDeleteFileProcessor
    {
        void Execute(IFileInfo slaveFile);
    }
}