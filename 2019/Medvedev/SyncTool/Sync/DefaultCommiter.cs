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
        protected Dictionary<Type, Action<IResolution>> _resolutionCommits;

        public DefaultCommiter(IProvider provider, Logger logger)
        {
            _resolutionCommits = new Dictionary<Type, Action<IResolution>>
            {
                {typeof(UpdateResolution), CommitUpdateResolution},
                {typeof(CopyResolution), CommitCopyResolution},
                {typeof(DeleteResolution), CommitDeleteResolution}
            };

            _provider = provider;
            _logger = logger;
        }

        public void Commit(IEnumerable<IResolution> resolutions)
        {
            foreach (var resolution in resolutions)
                _resolutionCommits[resolution.GetType()].Invoke(resolution);
        }

        private void CommitUpdateResolution(IResolution r)
        {
            var resolution = (UpdateResolution)r;

            _provider.Delete(resolution.Destination);
            _provider.CopyTo(resolution.Source, resolution.Destination.FullName);
            _logger.Log(resolution);
        }

        private void CommitCopyResolution(IResolution r)
        {
            var resolution = (CopyResolution)r;

            _provider.CopyTo(resolution.Source, resolution.Destination);
            _logger.Log(resolution);
        }

        private void CommitDeleteResolution(IResolution r)
        {
            var resolution = (DeleteResolution)r;

            _provider.Delete(resolution.Source);
            _logger.Log(resolution);
        }
    }
}