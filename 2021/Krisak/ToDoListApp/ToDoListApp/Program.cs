using ToDoLibrary;
using ToDoLibrary.Loggers;

namespace Todo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var toDoApp = new ToDoApp(logger);
            if (args.Length == 0)
            {
                toDoApp.WorkWithConsole(new UserInputFromConsole());
            }
            else
            {
                toDoApp.WorkWithConsole(new UserInputFromCmd(args));
            }
        }
    }
}
