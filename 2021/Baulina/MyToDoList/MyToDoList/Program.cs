using System;
using Spectre.Console;

namespace ToDoListConsole
{
    class Program
    {
        static void Main()
        {
            var uploadProcessor = new StartProcessor();
            uploadProcessor.LoadTheList();
            var menuManager = new MenuManager(uploadProcessor.MyToDoList, new MessagePrinter());
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
            var operation = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold lightgoldenrod2_1] What do you want to do? [/]")
                    .PageSize(12)
                    .MoreChoicesText("[grey](Move up and down to reveal more operations)[/]")
                    .AddChoices("Add", "Edit", "Mark as complete", "Delete", "View all tasks", "Exit"));
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
                default:
                {
                    manager.PrintErrorMessage();
                    break;
                }
            }
        }

    }
}
