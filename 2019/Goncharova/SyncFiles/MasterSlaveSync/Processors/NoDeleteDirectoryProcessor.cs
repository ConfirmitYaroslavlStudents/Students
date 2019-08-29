using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class NoDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public void Execute(IDirectoryInfo slaveDirectory)
        {
        }

    }
}
