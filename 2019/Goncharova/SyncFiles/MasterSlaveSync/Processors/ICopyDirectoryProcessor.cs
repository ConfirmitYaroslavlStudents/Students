using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface ICopyDirectoryProcessor
    {
        void Execute(IDirectoryInfo masterDirectory, IDirectoryInfo target);
    }
}