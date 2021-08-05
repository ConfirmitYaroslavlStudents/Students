using System.Threading.Tasks;

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
                var CMDInputHandler= new CMDInputHandler(logger, args, client);
                await CMDInputHandler.HandleUsersInput();
            }
            else
            {
                var menuInputHandler = new MenuInputHandler(logger, reader, client);
                await menuInputHandler.HandleUsersInput();
            }
        }
    }
}