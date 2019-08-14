using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public bool Execute(IDirectoryInfo slaveDirectory)
        {
            slaveDirectory.Delete();

            return true;
        }
    }
}
