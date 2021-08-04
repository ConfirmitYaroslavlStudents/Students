namespace ToDoListClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            if (args.Length != 0)
            {
                var CMDInputHandler= new CMDInputHandler(logger, args);
                CMDInputHandler.HandleUsersInput();
            }
            else
            {
                var menuInputHandler = new MenuInputHandler(logger, reader);
                menuInputHandler.HandleUsersInput();
            }
        }
    }
}