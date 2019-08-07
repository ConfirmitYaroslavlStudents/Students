using Sync.Resolutions;
using Sync.Wrappers;

namespace Sync.ResolvingPolicies
{
    public class DefaultResolvingPolicy : IResolvingPolicy
    {
        public DirectoryWrapper Master { get; }
        public DirectoryWrapper Slave { get; }

        public DefaultResolvingPolicy(DirectoryWrapper master, DirectoryWrapper slave)
        {
            Master = master;
            Slave = slave;
        }

        public IResolution Resolve(Conflict conflict)
        {
            throw new System.NotImplementedException();
        }
    }
}