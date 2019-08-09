using System.IO.Abstractions;

namespace MasterSlaveSync
{
    class NoDeleteFileProcessor : IDeleteFileProcessor
    {
        public void Execute(IFileInfo slaveFile)
        {

        }
    }
}
