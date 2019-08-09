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
            if (resolution is UpdateResolution updateResolution)
                CommitUpdateResolution(updateResolution);
            if (resolution is CopyResolution copyResolution)
                CommitCopyResolution(copyResolution);
            if (resolution is DeleteResolution deleteResolution)
                CommitDeleteResolution(deleteResolution);
        }

        private void CommitUpdateResolution(UpdateResolution resolution)
        {
            _interactor.Delete(resolution.Destination);
            _interactor.CopyTo(resolution.Source, resolution.Destination.FullName);
            _logger?.Log(resolution);
        }

        private void CommitCopyResolution(CopyResolution resolution)
        {
            _interactor.CopyTo(resolution.Source, resolution.Destination);
            _logger?.Log(resolution);
        }

        private void CommitDeleteResolution(DeleteResolution resolution)
        {
            _interactor.Delete(resolution.Source);
            _logger?.Log(resolution);
        }
    }
}