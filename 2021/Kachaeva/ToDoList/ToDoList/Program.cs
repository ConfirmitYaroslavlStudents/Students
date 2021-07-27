namespace ToDo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var fileWorkHandler = new FileLoaderSaver(fileName);
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            if (args.Length != 0)
            {
                var CMDHandler= new CMDHandler(fileWorkHandler, logger, args);
                CMDHandler.HandleUsersInput();
            }
            else
            {
                var menuCommandHandler = new MenuCommandHandler(fileWorkHandler, logger, reader);
                menuCommandHandler.HandleUsersInput();
            }
        }
    }
}