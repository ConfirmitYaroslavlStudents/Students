using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ToDoClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            var client = new WrappedHttpClient();
            if (args.Length != 0)
            {
                var cmdInputHandler = new CmdInputHandler(logger, args, client);
                await cmdInputHandler.HandleUsersInput();
            }
            else
            {
                var menuInputHandler = new MenuInputHandler(logger, reader, client);
                await menuInputHandler.HandleUsersInput();
            }
        }
    }
}