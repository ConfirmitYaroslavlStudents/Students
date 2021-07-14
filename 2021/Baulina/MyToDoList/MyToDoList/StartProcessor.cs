using System;
using MyToDoList;
using Spectre.Console;

namespace ToDoListConsole
{
    class StartProcessor
    {
        public ToDoList MyToDoList { get; private set; }

        internal bool Empty => MyToDoList.IsEmpty;
        public void Process()
        {
            var firstRun = FirstExecutionDeterminer.FirstRun;
            if (!firstRun)
            {
                LoadTheList();
                NotFirstRunCase();
            }
            else
                FirstRunCase();
        }
        
        public void LoadTheList()
        {
            var toDoItemsArray = DataSerializer.Deserialize();
            MyToDoList = new ToDoList(toDoItemsArray);
        }

        public  void FirstRunCase()
        {
            AnsiConsole.MarkupLine("[bold lightgoldenrod2_1]Welcome to ToDoList! You need to add the first note to start.[/]");
        }

        public  void NotFirstRunCase()
        {
            AnsiConsole.MarkupLine("[bold lightgoldenrod2_1] Nice to see you again! :) [/]");
            switch (Empty)
            {
                case true:
                {
                    AnsiConsole.MarkupLine("However, your ToDoList is empty :( You might want to add something first. ");
                    break;
                }                
                default:
                {
                    Console.WriteLine("Your tasks have been uploaded. Choose 'View all tasks' to see");
                    break;
                }
            }
        }
    }
}
