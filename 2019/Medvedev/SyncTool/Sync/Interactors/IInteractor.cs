using Sync.Wrappers;

namespace Sync.Interactors
{
    public interface IInteractor
    {
        void Delete(IFileSystemElementWrapper element);
        void CopyTo(IFileSystemElementWrapper element, string path);
    }
}