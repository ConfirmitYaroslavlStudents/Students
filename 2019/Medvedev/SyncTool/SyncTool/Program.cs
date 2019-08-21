using System;
using NDesk.Options;
using Sync;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Interactors;
using Sync.Loggers;
using Sync.Providers;
using Sync.ResolvingPolicies;

namespace SyncTool
{
    internal class Program
    {
        private static SynchronizerBuilder _synchronizerBuilder;

        private static void Main(string[] args)
        {
            _synchronizerBuilder = new SynchronizerBuilder();

            SetupOptions().Parse(args);

            _synchronizerBuilder
                .SetConflictDetectionPolicy(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()))
                .SetProvider(new LocalDiskProvider())
                .SetInteractor(new LocalDiskInteractor())
                .Synchronize();
        }

        private static OptionSet SetupOptions()
        {
            var optionSet = new OptionSet
            {
                {"M|master=", "path to master directory", path => _synchronizerBuilder.SetMaster(path)},
                {"S:|slave:=", "path to slave directory",
                    (path, param) => _synchronizerBuilder.AddSlave(path, 
                        param == null ? 
                            ResolverOptions.None : 
                            ResolverOptions.NoDelete)},
                {
                    "silent", "no log output", x =>
                    {
                        if (x != null)
                            _synchronizerBuilder.SetLogger(new StreamLogger(Console.Out, LoggerOption.Silent));
                    }
                },
                {
                    "summary", "short log output", x =>
                    {
                        if (x != null)
                            _synchronizerBuilder.SetLogger(new StreamLogger(Console.Out, LoggerOption.Summary));
                    }
                },
                {
                    "verbose", "full log output", x =>
                    {
                        if (x != null)
                            _synchronizerBuilder.SetLogger(new StreamLogger(Console.Out, LoggerOption.Verbose));
                    }
                }
            };

            return optionSet;
        }
    }
}