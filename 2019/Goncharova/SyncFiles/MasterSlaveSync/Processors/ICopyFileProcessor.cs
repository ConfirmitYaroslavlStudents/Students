using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface ICopyFileProcessor
    {
        void Execute(IFileInfo masterFile, string masterPath, string slavePath);
    }
}