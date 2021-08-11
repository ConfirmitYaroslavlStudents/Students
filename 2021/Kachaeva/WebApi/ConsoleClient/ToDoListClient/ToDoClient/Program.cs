using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ToDoClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            var url = config.GetSection("Url").Value;

            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            var client = new WrappedHttpClient(url);
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