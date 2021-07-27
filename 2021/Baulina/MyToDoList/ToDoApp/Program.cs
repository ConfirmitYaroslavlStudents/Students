using InputOutputManagers;
using MyToDoList;
using FileCommunicator;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var isCommandLineExecuted = args.Length != 0;
            IConsoleExtended console = isCommandLineExecuted
                ? new CommandLineInteractor(args)
                : new ConsoleInteractor();
            var toDoList = new ToDoList(new FileManager().LoadFromFile());
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);
            AppEngine appEngine =
                isCommandLineExecuted
                    ? new CommandLineProcessor(appController)
                    : new ConsoleMenuProcessor(appController);

            appEngine.Run();
        }
    }
}
