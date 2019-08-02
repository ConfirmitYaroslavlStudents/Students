using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class CopyResolution : IResolution
    {
        public IFileSystemElementWrapper Source { get; }
        public string Destination { get; }

        public CopyResolution(IFileSystemElementWrapper src, string dst)
        {
            Source = src;
            Destination = dst;
        }
    }
}