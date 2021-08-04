using System.Threading.Tasks;

namespace ToDoClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            if (args.Length != 0)
            {
                var CMDInputHandler= new CMDInputHandler(logger, args);
                await CMDInputHandler.HandleUsersInput();
            }
            else
            {
                var menuInputHandler = new MenuInputHandler(logger, reader);
                await menuInputHandler.HandleUsersInput();
            }
        }
    }
}