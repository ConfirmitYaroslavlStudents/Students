using Sync.Resolutions;
using Sync.Wrappers;

namespace Sync.ResolvingPolicies
{
    public class NoDeleteResolvingPolicy : IResolvingPolicy
    {
        public DirectoryWrapper Master { get; }
        public DirectoryWrapper Slave { get; }

        public NoDeleteResolvingPolicy(DirectoryWrapper master, DirectoryWrapper slave)
        {
            Master = master;
            Slave = slave;
        }

        public virtual IResolution Resolve(Conflict conflict)
        {
            if (ExistsUpdateResolution(conflict))
                return MakeUpdateResolution(conflict);
            if (ExistsCopyResolution(conflict))
                return MakeCopyResolution(conflict);
            return null;
        }

        private bool ExistsUpdateResolution(Conflict conflict)
        {
            return conflict.Source != null && conflict.Destination != null;
        }

        private IResolution MakeUpdateResolution(Conflict conflict)
        {
            return new UpdateResolution(conflict.Source, conflict.Destination);
        }

        private bool ExistsCopyResolution(Conflict conflict)
        {
            return conflict.Source != null && conflict.Destination == null;
        }

        private IResolution MakeCopyResolution(Conflict conflict)
        {
            var path = conflict.Source.FullName.Replace(Master.FullName, Slave.FullName);

            return new CopyResolution(conflict.Source, path);
        }
    }
}