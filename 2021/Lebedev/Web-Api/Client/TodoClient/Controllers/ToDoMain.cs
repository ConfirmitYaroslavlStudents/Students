using System.IO;
using MyTODO;
using MyTODO.Controllers;

namespace ToDoClient.Controllers
{
    internal class ToDoMain
    {
        private static void Main(string[] args)
        {
            var manager = new ToDoFileManager(new FileInfo("TODOsave.txt"));
            var todo = new ToDoList(manager.Read());
            if (args.Length != 0)
            {
                var argsWorker = new ToDoArgs(todo);
                argsWorker.WorkWithArgs(args);
                manager.Save(todo);
                return;
            }
            var menu = new ToDoMenu(new ToDoApiConnector());
            while (true)
            {
                menu.PrintMenu();
                menu.WorkWithMenu();
                manager.Save(todo);
            }
        }
    }
}
