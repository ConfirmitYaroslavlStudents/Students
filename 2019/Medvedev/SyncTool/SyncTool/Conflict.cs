using System.IO;
using SyncTool.Wrappers;

namespace SyncTool
{
    public class Conflict
    {
        public IFileSystemElement Source { get; }
        public IFileSystemElement Destination { get; }

        public Conflict(IFileSystemElement first, IFileSystemElement second)
        {
            var comparision = first.CompareTo(second);

            if (comparision < 0)
            {
                Source = first;
                Destination = second;
            }

            if (comparision > 0)
            {
                Source = second;
                Destination = first;
            }
        }
    }
}