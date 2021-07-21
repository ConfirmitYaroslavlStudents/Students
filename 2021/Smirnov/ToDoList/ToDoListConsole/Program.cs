using ToDoListLib;

namespace ToDoListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var saveLoader = new SaveLoader();

            ConsoleMenu toDoListMenu;

            if (args.Length == 0)
            {
                toDoListMenu = new ConsoleMenu(saveLoader.Load(), new ConsoleReader());
                while (toDoListMenu.StartMenu()) ;
            }
            else
            {
                toDoListMenu = new ConsoleMenu(saveLoader.Load(), new CmdReader(args));
                toDoListMenu.StartMenu();
            }

            saveLoader.Save(toDoListMenu.ToDoList);
        }
    }
}
