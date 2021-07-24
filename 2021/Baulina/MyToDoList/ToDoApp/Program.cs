using InputOutputManagers;
using MyToDoList;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var isCommandLineExecuted = args.Length != 0;
            var inputOutputManager = isCommandLineExecuted
                ? new InputOutputManager(new CommandLineInteractor(args))
                : new InputOutputManager(new ConsoleInteractor());
            var toDoList = new ToDoList(new FileManager().LoadFromFile());
            var commandExecutor = new CommandExecutor(toDoList, inputOutputManager);
            var appController = new AppController(commandExecutor, inputOutputManager);
            AppEngine appEngine =
                isCommandLineExecuted ? new CommandLineProcessor(appController) : new ConsoleMenuProcessor(appController);

            appEngine.Run();
        }
    }
}
