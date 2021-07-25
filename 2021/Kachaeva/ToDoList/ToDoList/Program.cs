namespace ToDo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var fileWorkHandler = new FileWorkHandler(fileName);
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            CommandHandler handler;
            if (args.Length != 0)
                handler = new CMDHandler(fileWorkHandler,logger, args);
            else
                handler = new MenuCommandHandler(fileWorkHandler, logger, reader);
            handler.HandleUsersInput();
        }
    }
}