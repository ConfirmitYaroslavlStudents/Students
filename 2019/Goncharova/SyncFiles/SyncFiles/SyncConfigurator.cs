using MasterSlaveSync;
using System;
using System.Collections.Generic;

namespace Sync
{
    public class SyncConfigurator
    {
        private readonly string[] args;
        private readonly List<string> options = new List<string>();

        public SyncConfigurator(string[] arguments)
        {
            args = arguments;
            ParseArgs();
        }

        public string Master { get; private set; }
        public string Slave { get; private set; }
        public string ErrorMessage { get; private set; } = String.Empty;

        public SynchronizerBuilder GetSynchronizer()
        {
            var synchronizer = new SynchronizerBuilder(Master, Slave);

            foreach (var option in options)
            {
                switch (option)
                {
                    case "nodelete":
                        synchronizer.NoDelete();
                        break;
                    case "logsummary":
                        synchronizer.LogSummary(Console.WriteLine);
                        break;
                    case "logverbose":
                        synchronizer.LogVerbose(Console.WriteLine);
                        break;
                    default:
                        ErrorMessage = "Unknown option";
                        break;
                }
            }

            return synchronizer;
        }

        private void ParseArgs()
        {
            Master = args[1];
            Slave = args[2];

            for (int i = 3; i < args.Length; i++)
            {
                options.Add(args[i].Substring(1).ToLower());
            }
        }
    }
}
