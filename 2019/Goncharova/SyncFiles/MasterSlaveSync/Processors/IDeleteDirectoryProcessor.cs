using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteDirectoryProcessor
    {
        void Execute(IDirectoryInfo slaveDirectory);
    }
}