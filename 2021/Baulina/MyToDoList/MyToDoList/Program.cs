using ConsoleInteractors;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var uploadProcessor = new StartProcessor();
            uploadProcessor.LoadTheList();
            var isCommandLineExecuted = args.Length != 0;
            var consoleHandler = isCommandLineExecuted
                ? new ConsoleHandler(new CommandLineHandler(args))
                : new ConsoleHandler(new MyConsole());
            var commandExecutor = new CommandExecutor(uploadProcessor.MyToDoList, consoleHandler);
            var menuPrinter = new MenuHandler(new OperationGetter(consoleHandler), commandExecutor);
            IMenuProcessor processor =
                isCommandLineExecuted ? new CommandLineProcessor(menuPrinter) : new ConsoleMenuProcessor(menuPrinter);

            processor.Run();
        }
    }
}
