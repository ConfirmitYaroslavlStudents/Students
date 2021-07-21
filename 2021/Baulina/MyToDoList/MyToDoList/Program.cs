namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var uploadProcessor = new StartProcessor();
            uploadProcessor.LoadTheList();
            var isCommandLineExecuted = args.Length != 0;
            var messagePrinter = isCommandLineExecuted
                ? new MessagePrinter(new CommandLineHandler(args))
                : new MessagePrinter(new MyConsole());
            IMenuProcessor menuProcessor =
                isCommandLineExecuted ? new CommandLineProcessor() : new ConsoleMenuProcessor();
            var menuManager = new MenuManager(uploadProcessor.MyToDoList, messagePrinter);
            menuProcessor.MenuManager = menuManager;

            menuProcessor.Run();
        }
    }
}
