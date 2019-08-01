using System.IO;

namespace SyncTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = new DirectoryInfo(args[0]);
            var slave = new DirectoryInfo(args[1]);

            var collector = new ConflictsCollector(master, slave);

            var resolverOption = ResolverOptions.None;
            if (args.Length == 3 && args[2] == "--nodelete")
                resolverOption = ResolverOptions.NoDelete;

            var resolver = new Resolver(master, slave, resolverOption);

            resolver.ResolveConflicts(collector.GetConflicts());
        }
    }
}