using SyncLib;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Synchronization synchronization = new Synchronization(args[0], args[1]);
            try
            {
                synchronization.SetNoDeleteOption(args[2] == "noDelete");

                switch (args[3])
                {
                    case "summary":
                        synchronization.SetLogOption(EnumLog.Summary);
                        break;
                    case "verbose":
                        synchronization.SetLogOption(EnumLog.Verbose);
                        break;
                }
            }
            catch
            { }

            synchronization.Synchronaze();
        }
    }
}
