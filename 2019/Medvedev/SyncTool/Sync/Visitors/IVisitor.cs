using Sync.Resolutions;

namespace Sync.Visitors
{
    public interface IVisitor
    {
        void Visit(CopyResolution resolution);
        void Visit(DeleteResolution resolution);
        void Visit(UpdateResolution resolution);
    }
}