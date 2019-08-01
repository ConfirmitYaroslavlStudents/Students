using System.Collections.Generic;
using System.IO;

namespace SyncTool
{
    public class Resolver
    {
        public DirectoryInfo MasterDirectory { get; }
        public DirectoryInfo SlaveDirectory { get; }

        public ResolverOptions Option { get; }

        public Resolver(DirectoryInfo master, DirectoryInfo slave, ResolverOptions option = ResolverOptions.None)
        {
            MasterDirectory = master;
            SlaveDirectory = slave;
            Option = option;
        }

        public void ResolveConflicts(List<Conflict> conflicts)
        {
            foreach (var conflict in conflicts)
            {
                if (ExistsUpdateResolution(conflict))
                    ResolveViaUpdate(conflict);
                if (ExistsDeleteResolution(conflict) && Option != ResolverOptions.NoDelete)
                    ResolveViaDelete(conflict);
                if (ExistsCopyResolution(conflict))
                    ResolveViaCopy(conflict);
            }
        }

        private bool ExistsUpdateResolution(Conflict conflict)
        {
            return conflict.Source != null && conflict.Destination != null;
        }

        private void ResolveViaUpdate(Conflict conflict)
        {
            conflict.Destination.Delete();
            conflict.Source.CopyTo(conflict.Destination.GetPath());
        }

        private bool ExistsCopyResolution(Conflict conflict)
        {
            return conflict.Source != null && conflict.Destination == null;
        }

        private void ResolveViaCopy(Conflict conflict)
        {
            var path = conflict.Source.GetPath().Replace(MasterDirectory.FullName, SlaveDirectory.FullName);
            conflict.Source.CopyTo(path);
        }

        private bool ExistsDeleteResolution(Conflict conflict)
        {
            return conflict.Source == null && conflict.Destination != null;
        }

        private void ResolveViaDelete(Conflict conflict)
        {
            conflict.Destination.Delete();
        }
    }
}