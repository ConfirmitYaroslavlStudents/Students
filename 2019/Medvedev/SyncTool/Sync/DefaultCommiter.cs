using System;
using System.Collections.Generic;
using Sync.Loggers;
using Sync.Providers;
using Sync.Resolutions;

namespace Sync
{
    public class DefaultCommiter 
    {
        private Logger _logger;
        private IProvider _provider;

        public DefaultCommiter(IProvider provider, Logger logger)
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
            if (resolution is UpdateResolution ur)
                CommitUpdateResolution(ur);
            if (resolution is CopyResolution cp)
                CommitCopyResolution(cp);
            if (resolution is DeleteResolution dr)
                CommitDeleteResolution(dr);
        }

        private void CommitUpdateResolution(UpdateResolution resolution)
        {
            _provider.Delete(resolution.Destination);
            _provider.CopyTo(resolution.Source, resolution.Destination.FullName);
            _logger.Log(resolution);
        }

        private void CommitCopyResolution(CopyResolution resolution)
        {
            _provider.CopyTo(resolution.Source, resolution.Destination);
            _logger.Log(resolution);
        }

        private void CommitDeleteResolution(DeleteResolution resolution)
        {
            _provider.Delete(resolution.Source);
            _logger.Log(resolution);
        }
    }
}