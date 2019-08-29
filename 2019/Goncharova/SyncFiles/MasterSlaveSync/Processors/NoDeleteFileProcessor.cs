using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class NoDeleteFileProcessor : IDeleteFileProcessor
    {
        public void Execute(IFileInfo slaveFile)
        {
        }
    }
}
