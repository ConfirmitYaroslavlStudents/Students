using ToDoListApp.Client;
using ToDoListApp.Reader;
using ToDoListApp.Writer;

namespace ToDoListApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var toDoListMenu = args.Length == 0 ? new Menu(new HttpRequestGenerator(), new UserInputReader(), new ConsoleWriter()) : 
                                                  new Menu(new HttpRequestGenerator(), new CommandReader(args), new ConsoleWriter());
            toDoListMenu.StartMenu();
        }
    }
}
