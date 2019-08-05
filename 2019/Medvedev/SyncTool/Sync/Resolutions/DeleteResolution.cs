using Sync.Wrappers;

namespace Sync.Resolutions
{
    public class DeleteResolution : IResolution
    {
        public readonly IFileSystemElementWrapper Source;

        public DeleteResolution(IFileSystemElementWrapper src)
        {
            Source = src;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is DeleteResolution))
                return false;

            var other = (DeleteResolution) obj;

            return Equals(Source, other.Source);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }
    }
}