using System.IO;
using Sync;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;

namespace SyncTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = new DirectoryInfo(args[0]);
            var slave = new DirectoryInfo(args[1]);

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var resolverOption = ResolverOptions.None;
            if (args.Length == 3 && args[2] == "--nodelete")
                resolverOption = ResolverOptions.NoDelete;

            var resolver = new Resolver(master, slave, resolverOption);

            resolver.GetConflictsResolutions(collector.GetConflicts());
        }
    }
}