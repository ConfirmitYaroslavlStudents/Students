using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class UpdateResolution : IResolution
    {
        public UpdateResolution(IFileSystemElementWrapper src, IFileSystemElementWrapper dst)
        {
            Source = src;
            Destination = dst;
        }

        public IFileSystemElementWrapper Source { get; }
        public IFileSystemElementWrapper Destination { get; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is UpdateResolution))
                return false;

            var other = (UpdateResolution) obj;

            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() ^ Destination.GetHashCode();
        }
    }
}