using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class DeleteResolution : IResolution
    {
        public IFileSystemElementWrapper Source;

        public DeleteResolution(IFileSystemElementWrapper src)
        {
            Source = src;
        }
    }
}