using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface IDeleteDirectoryProcessor
    {
        void Execute(IDirectoryInfo slaveDirectory);
    }
}