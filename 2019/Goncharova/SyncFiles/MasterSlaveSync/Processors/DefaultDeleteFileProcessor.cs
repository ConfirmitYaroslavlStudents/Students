using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultDeleteFileProcessor : IDeleteFileProcessor
    {
        public void Execute(IFileInfo slaveFile)
        {
            slaveFile.Delete();
        }
    }
}
