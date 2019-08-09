using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public void Execute(IDirectoryInfo slaveDirectory)
        {
            slaveDirectory.Delete();
        }
    }
}
