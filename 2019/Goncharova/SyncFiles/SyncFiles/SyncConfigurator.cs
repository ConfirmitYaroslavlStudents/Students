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
        public List<string> Slaves { get; private set; } = new List<string>();
        public string ErrorMessage { get; private set; } = String.Empty;

        public Synchronizator GetSynchronizator()
        {
            var options = GetSyncOptions();

            var synchronizator = new Synchronizator(Master, Slaves[0], options);

            for (int i = 1; i < Slaves.Count; i++)
            {
                synchronizator.AddSlave(Slaves[i]);
            }

            return synchronizator;
        }

        private SyncOptions GetSyncOptions()
        {
            var syncOptions = new SyncOptions();

            foreach (var option in options)
            {
                switch (option)
                {
                    case "nodelete":
                        syncOptions.NoDeleteOn();
                        break;
                    case "logsummary":
                        syncOptions.LogSummary(Console.WriteLine);
                        break;
                    case "logverbose":
                        syncOptions.LogVerbose(Console.WriteLine);
                        break;
                    case "logsilent":
                        syncOptions.LogSilent();
                        break;
                    default:
                        ErrorMessage = "Unknown option";
                        break;
                }
            }

            return syncOptions;
        }

        private void ParseArgs()
        {
            Master = args[1];

            for (int i = 2; i < args.Length; i++)
            {
                if(args[i].StartsWith("-"))
                {
                    options.Add(args[i].Substring(1).ToLower());
                }
                else
                {
                    Slaves.Add(args[i]);
                }
            }
        }
    }
}
