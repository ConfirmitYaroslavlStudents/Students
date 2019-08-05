using System.Collections.Generic;
using Sync.Resolutions;
using Sync.Wrappers;

namespace Sync
{
    public class Resolver
    {
        public Resolver(DirectoryWrapper master, DirectoryWrapper slave, ResolverOptions option = ResolverOptions.None)
        {
            MasterDirectory = master;
            SlaveDirectory = slave;
            Option = option;
        }

        public DirectoryWrapper MasterDirectory { get; }
        public DirectoryWrapper SlaveDirectory { get; }
        public ResolverOptions Option { get; }

        public List<IResolution> GetConflictsResolutions(List<Conflict> conflicts)
        {
            var resolutions = new List<IResolution>();
            foreach (var conflict in conflicts)
            {
                if (ExistsUpdateResolution(conflict))
                    resolutions.Add(MakeUpdateResolution(conflict));
                if (ExistsDeleteResolution(conflict) && Option != ResolverOptions.NoDelete)
                    resolutions.Add(MakeDeleteResolution(conflict));
                if (ExistsCopyResolution(conflict))
                    resolutions.Add(MakeCopyResolution(conflict));
            }

            return resolutions;
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
            var path = conflict.Source.FullName.Replace(MasterDirectory.FullName, SlaveDirectory.FullName);

            return new CopyResolution(conflict.Source, path);
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