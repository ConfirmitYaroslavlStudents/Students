using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class NoDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public void Execute(IDirectoryInfo slaveDirectory)
        {
        }
    }
}
