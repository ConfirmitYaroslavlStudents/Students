using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class NoDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public bool Execute(IDirectoryInfo slaveDirectory)
        {
            return false;
        }
    }
}
