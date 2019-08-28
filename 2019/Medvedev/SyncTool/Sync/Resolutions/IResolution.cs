using Sync.Interactors;
using Sync.Visitors;

namespace Sync.Resolutions
{
    public interface IResolution
    {
        void Commit(IInteractor interactor);
        void Accept(IVisitor visitor);
    }
}