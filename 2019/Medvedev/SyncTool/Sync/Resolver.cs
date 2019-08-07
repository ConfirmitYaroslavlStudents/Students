using System.Collections.Generic;
using Sync.Resolutions;
using Sync.ResolvingPolicies;
using Sync.Wrappers;

namespace Sync
{
    public class Resolver
    {
        private IResolvingPolicy _resolvingPolicy;
        public Resolver(IResolvingPolicy policy)
        {
            _resolvingPolicy = policy;
        }

        public List<IResolution> GetConflictsResolutions(List<Conflict> conflicts)
        {
            var resolutions = new List<IResolution>();
            foreach (var conflict in conflicts)
            {
                var resolution = _resolvingPolicy.Resolve(conflict);

                if (resolution != null)
                    resolutions.Add(resolution);
            }

            return resolutions;
        }
    }
}