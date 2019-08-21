using Sync.Resolutions;
using Sync.Wrappers;

namespace Sync.ResolvingPolicies
{
    public class DefaultResolvingPolicy : NoDeleteResolvingPolicy
    {
        public DefaultResolvingPolicy(DirectoryWrapper master, DirectoryWrapper slave) 
            : base(master, slave)
        {
        }

        public override IResolution Resolve(Conflict conflict)
        {
            var resolution = base.Resolve(conflict);

            if (resolution != null)
                return resolution;

            if (ExistsDeleteResolution(conflict))
                return MakeDeleteResolution(conflict);

            return null;
        }

        private bool ExistsDeleteResolution(Conflict conflict)
        {
            return conflict.Source == null && conflict.Destination != null;
        }

        private IResolution MakeDeleteResolution(Conflict conflict)
        {
            return new DeleteResolution(conflict.Destination);
        }
    }
}