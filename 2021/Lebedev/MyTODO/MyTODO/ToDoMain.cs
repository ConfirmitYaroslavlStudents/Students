using System.IO;

namespace MyTODO
{
    internal class ToDoMain
    {
        private static void Main(string[] args)
        {
            var restorer = new ToDoListRestorer(new FileInfo("TODOsave.txt"));
            var todo = new ToDoList(restorer.Read());
            if (args.Length != 0)
            {
                var argsWorker = new ToDoArgs(todo);
                argsWorker.WorkWithArgs(args);
                restorer.Save(todo);
                return;
            }
            var menu = new ToDoMenu(todo);
            while (true)
            {
                menu.PrintMenu();
                menu.WorkWithMenu();
                restorer.Save(todo);
            }
        }
    }
}
