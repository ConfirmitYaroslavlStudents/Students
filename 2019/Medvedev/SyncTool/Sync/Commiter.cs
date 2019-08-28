using System.Collections.Generic;
using Sync.Interactors;
using Sync.Resolutions;
using Sync.Visitors;

namespace Sync
{
    public class Commiter
    {
        private readonly IVisitor _visitor;
        private readonly IInteractor _interactor;

        public Commiter(IInteractor interactor, IVisitor visitor = null)
        {
            _interactor = interactor;
            _visitor = visitor;
        }

        public void Commit(IEnumerable<IResolution> resolutions)
        {
            foreach (var resolution in resolutions)
                CommitResolution(resolution);
        }

        protected virtual void CommitResolution(IResolution resolution)
        {
            resolution.Commit(_interactor);
            resolution.Accept(_visitor);
        }
    }
}