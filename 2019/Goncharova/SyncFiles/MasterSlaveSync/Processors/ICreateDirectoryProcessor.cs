using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface ICreateDirectoryProcessor
    {
        void Execute(IDirectoryInfo masterDirectory, string masterPath, string slavePath);
    }
}