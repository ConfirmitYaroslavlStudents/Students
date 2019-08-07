using System;
using NDesk.Options;
using Sync;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Loggers;
using Sync.Providers;
using Sync.ResolvingPolicies;

namespace SyncTool
{
    internal class Program
    {
        private static Parameters _parameters;

        private static void Main(string[] args)
        {
            _parameters = new Parameters();
            var options = SetupOptions();
            options.Parse(args);

            var provider = new LocalDiskProvider();
            var master = provider.LoadDirectory(_parameters.PathToMaster);
            var slave = provider.LoadDirectory(_parameters.PathToSlave);

            var conflicts = new ConflictsCollector(
                    master,
                    slave,
                    new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()))
                .GetConflicts();

            IResolvingPolicy resolvingPolicy = null;

            if (_parameters.ResolverOption == ResolverOptions.NoDelete)
                resolvingPolicy = new NoDeleteResolvingPolicy(master, slave);
            else
                resolvingPolicy = new DefaultResolvingPolicy(master, slave);

            var resolutions = new Resolver(resolvingPolicy)
                .GetConflictsResolutions(conflicts);

            var commiter = new Commiter(provider, new StreamLogger(Console.Out, _parameters.LoggerOption));

            commiter.Commit(resolutions);
        }

        private static OptionSet SetupOptions()
        {
            var optionSet = new OptionSet
            {
                {"m|master=", "path to master directory", path => _parameters.PathToMaster = path},
                {"s|slave=", "path to slave directory", path => _parameters.PathToSlave = path},
                {
                    "no-delete", "slave files that have no match in master will not delete", x =>
                    {
                        if (x != null)
                            _parameters.ResolverOption = ResolverOptions.NoDelete;
                    }
                },
                {
                    "silent", "no log output", x =>
                    {
                        if (x != null)
                            _parameters.LoggerOption = LoggerOption.Silent;
                    }
                },
                {
                    "summary", "short log output", x =>
                    {
                        if (x != null)
                            _parameters.LoggerOption = LoggerOption.Summary;
                    }
                },
                {
                    "verbose", "full log output", x =>
                    {
                        if (x != null)
                            _parameters.LoggerOption = LoggerOption.Verbose;
                    }
                }
            };

            return optionSet;
        }
    }
}