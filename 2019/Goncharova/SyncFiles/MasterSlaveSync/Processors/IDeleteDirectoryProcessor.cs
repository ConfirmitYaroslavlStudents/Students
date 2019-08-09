using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteDirectoryProcessor
    {
        bool Execute(IDirectoryInfo slaveDirectory);
    }
}