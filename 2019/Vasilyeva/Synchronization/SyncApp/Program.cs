using SyncLib;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MultipleSync sync = new MultipleSync(args[0]);

            int i = 1;

            while (i < args.Length - 1)
            {
                if (args[i + 1] == "--no-delete")
                {
                    sync.SetSlave(args[i], true);
                    i += 2;
                }
                else
                {
                    sync.SetSlave(args[i]);
                    i++;
                }
            }
            if (i == args.Length - 1)
                switch (args[i])
                {
                    case "silent":
                        sync.SetLoggerType(LoggerType.Silent);
                        break;
                    case "verbose":
                        sync.SetLoggerType(LoggerType.Verbose);
                        break;
                    case "summary":
                        sync.SetLoggerType(LoggerType.Summary);
                        break;
                    default:
                        sync.SetSlave(args[i]);
                        break;
                }

            sync.Synchronize();

        }
    }
}
