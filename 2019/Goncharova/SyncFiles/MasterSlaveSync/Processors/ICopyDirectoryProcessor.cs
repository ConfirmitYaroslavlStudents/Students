using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface ICopyDirectoryProcessor
    {
        event EventHandler<ResolverEventArgs> DirectoryCopied;

        void Execute(IDirectoryInfo masterDirectory, IDirectoryInfo target);
    }
}