namespace ToDo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var fileWorkHandler = new FileWorkHandler(fileName);
            var consoleWriterReader = new ConsoleWriterReader();
            CommandHandler handler;
            if (args.Length != 0)
                handler = new CMDHandler(fileWorkHandler,consoleWriterReader, args);
            else
                handler = new ConsoleHandler(fileWorkHandler, consoleWriterReader);
            handler.HandleUsersInput();
        }
    }
}