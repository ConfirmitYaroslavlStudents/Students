using System.Collections.Generic;
using Sync.Interactors;
using Sync.Loggers;
using Sync.Providers;
using Sync.Resolutions;

namespace Sync
{
    public class Commiter
    {
        private readonly Logger _logger;
        private readonly IInteractor _interactor;

        public Commiter(IInteractor interactor, Logger logger = null)
        {
            _interactor = interactor;
            _logger = logger;
        }

        public void Commit(IEnumerable<IResolution> resolutions)
        {
            foreach (var resolution in resolutions)
                CommitResolution(resolution);
        }

        protected virtual void CommitResolution(IResolution resolution)
        {
            resolution.Commit(_interactor);
        }
    }
}