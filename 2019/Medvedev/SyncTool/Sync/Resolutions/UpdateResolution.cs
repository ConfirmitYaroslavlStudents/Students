using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class UpdateResolution : IResolution
    {
        public IFileSystemElementWrapper Source { get; }
        public IFileSystemElementWrapper Destination { get; }

        public UpdateResolution(IFileSystemElementWrapper src, IFileSystemElementWrapper dst)
        {
            Source = src;
            Destination = dst;
        }
    }
}