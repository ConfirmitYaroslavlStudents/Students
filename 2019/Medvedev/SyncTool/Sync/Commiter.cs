using System.Collections.Generic;
using Sync.Loggers;
using Sync.Providers;
using Sync.Resolutions;

namespace Sync
{
    public class Commiter
    {
        private readonly Logger _logger;
        private readonly IProvider _provider;

        public Commiter(IProvider provider, Logger logger = null)
        {
            _provider = provider;
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
            _provider.Delete(resolution.Destination);
            _provider.CopyTo(resolution.Source, resolution.Destination.FullName);
            _logger?.Log(resolution);
        }

        private void CommitCopyResolution(CopyResolution resolution)
        {
            _provider.CopyTo(resolution.Source, resolution.Destination);
            _logger?.Log(resolution);
        }

        private void CommitDeleteResolution(DeleteResolution resolution)
        {
            _provider.Delete(resolution.Source);
            _logger?.Log(resolution);
        }
    }
}