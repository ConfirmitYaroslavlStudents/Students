namespace ToDoClient.Controllers
{
    internal class ToDoMain
    {
        private static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                var argsWorker = new ToDoArgs(new ToDoApiConnector());
                argsWorker.WorkWithArgs(args);
                return;
            }
            var menu = new ToDoConsoleMenu(new ToDoApiConnector());
            while (true)
            {
                menu.PrintMenu();
                menu.WorkWithMenu();
            }
        }
    }
}
