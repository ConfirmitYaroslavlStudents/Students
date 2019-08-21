using Sync.Interactors;

namespace Sync.Resolutions
{
    public interface IResolution
    {
        void Commit(IInteractor interactor);
    }
}