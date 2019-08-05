using Sync.Wrappers;

namespace Sync
{
    public class Conflict
    {
        public Conflict(IFileSystemElementWrapper source, IFileSystemElementWrapper destination)
        {
            Source = source;
            Destination = destination;
        }

        public IFileSystemElementWrapper Source { get; }
        public IFileSystemElementWrapper Destination { get; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is Conflict))
                return false;

            var other = (Conflict) obj;

            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override int GetHashCode()
        {
            if (Source is null)
                return Destination.GetHashCode();
            if (Destination is null)
                return Source.GetHashCode();
            return Source.GetHashCode() ^ Destination.GetHashCode();
        }
    }
}