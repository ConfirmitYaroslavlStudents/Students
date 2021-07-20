using System;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var uploadProcessor = new StartProcessor();
            uploadProcessor.LoadTheList();
            var console = new MyConsole();
            var menuManager = new MenuManager(uploadProcessor.MyToDoList, new MessagePrinter(console));
            AppDomain.CurrentDomain.ProcessExit += Exit;

            while (menuManager.IsWorking)
            {
                PrintMenu(menuManager);
            }

            void Exit(object sender, EventArgs e)
            {
                DataHandler.SaveToFile(menuManager.MyToDoList);
            }
        }

        public static void PrintMenu(MenuManager manager)
        {
            var operation = manager.GetMenuItemName();
            HandleOperation(operation, manager);
        }

        public static void HandleOperation(string operation, MenuManager manager)
        {
            switch (operation)
            {
                case "Add":
                {
                    manager.Add();
                    break;
                }
                case "Edit":
                {
                    manager.Edit();
                    break;
                }
                case "Mark as complete":
                {
                    manager.MarkAsComplete();
                    break;
                }
                case "Delete":
                {
                    manager.Delete();
                        break;
                }
                case "View all tasks":
                {
                    manager.ViewAllTasks();
                    break;
                }
                case "Exit":
                {
                    manager.Exit();
                    break;
                }
            }
        }

    }
}
