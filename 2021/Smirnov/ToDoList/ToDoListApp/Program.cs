using ToDoListApp.Reader;
using ToDoListApp.Writer;
using ToDoListLib.Controllers;
using ToDoListLib.SaveAndLoad;


namespace ToDoListApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var toDoListMenu = args.Length == 0 ? new Menu(new ToDoListController(new SaveAndLoadFromFile()), new UserInputReader(), new ConsoleWriter()) : 
                                                  new Menu(new ToDoListController(new SaveAndLoadFromFile()), new CommandReader(args), new ConsoleWriter());
            toDoListMenu.StartMenu();
        }
    }
}
