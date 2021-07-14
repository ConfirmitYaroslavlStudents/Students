using System;
using Spectre.Console;

namespace ToDoListConsole
{
    class Program
    {
        static void Main()
        {
            var uploadProcessor = new StartProcessor();
            uploadProcessor.Process();
            FirstExecutionDeterminer.DetermineWhetherItIsAFirstRun();
            var menuManager = new MenuManager(uploadProcessor.MyToDoList, new MessagePrinter());
            AppDomain.CurrentDomain.ProcessExit += Exit;

            while (true)
            {
                PrintMenu(menuManager);
            }
            
            void Exit(object sender, EventArgs e)
            {
                DataSerializer.Serialize(menuManager.MyToDoList);
            }
        }
        
        public static void PrintMenu(MenuManager manager)
        {
            var operation = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold plum3] What do you want to do? [/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more operations)[/]")
                    .AddChoices("Add", "Edit", "Mark as complete", "Delete", "View all tasks", "Exit"));
            OperationProcessor(operation, manager);
        }

        public static void OperationProcessor(string operation, MenuManager manager)
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
