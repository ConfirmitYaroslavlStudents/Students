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
            var appMenu = new ToDoAppMenu(new OperationGetter(inputOutputManager, commandExecutor), commandExecutor);
            AppEngine appEngine =
                isCommandLineExecuted ? new CommandLineProcessor(appMenu) : new ConsoleMenuProcessor(appMenu);

            appEngine.Run();
        }
    }
}
