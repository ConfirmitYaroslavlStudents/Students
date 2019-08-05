using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class CopyResolution : IResolution
    {
        public CopyResolution(IFileSystemElementWrapper src, string dst)
        {
            Source = src;
            Destination = dst;
        }

        public IFileSystemElementWrapper Source { get; }
        public string Destination { get; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is CopyResolution))
                return false;

            var other = (CopyResolution) obj;

            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() ^ Destination.GetHashCode();
        }
    }
}