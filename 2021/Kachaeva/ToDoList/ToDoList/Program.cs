namespace ToDo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var loaderAndSaver = new FileLoaderAndSaver(fileName);
            var logger = new ConsoleLogger();
            var reader = new ConsoleReader();
            if (args.Length != 0)
            {
                var CMDInputHandler= new CMDInputHandler(loaderAndSaver, logger, args);
                CMDInputHandler.HandleUsersInput();
            }
            else
            {
                var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);
                menuInputHandler.HandleUsersInput();
            }
        }
    }
}