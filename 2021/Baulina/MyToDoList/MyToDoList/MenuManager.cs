using System;
using MyToDoList;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoListConsole
{
    internal class MenuManager
    {
        public ToDoList MyToDoList { get; }
        private readonly MessagePrinter _messagePrinter;

        internal bool Empty => MyToDoList.IsEmpty;

        internal readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "ToDoList.txt");

        public MenuManager(ToDoList inputList, MessagePrinter printer)
        {
            MyToDoList = new ToDoList(inputList);
            _messagePrinter = printer;
        }

        public void Add()
        {
            var description = AnsiConsole.Prompt(
                new TextPrompt<string>("[plum3]What do you need to do? [/]")
                    .Validate(age =>
                    {
                        return age switch
                        {
                            null => ValidationResult.Error("[red]Incorrect task[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
            MyToDoList.Add(description);
            _messagePrinter.PrintDoneMessage();
        }

        public void Edit()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }
            ViewAllTasks();
            var taskNumber = ChooseTaskNumber();
            Console.WriteLine("Type in a new description");
            var newDescription = Console.ReadLine();
            MyToDoList.EditDescription(taskNumber, newDescription);
            _messagePrinter.PrintDoneMessage();
        }

        public void MarkAsComplete()
        {
            if (Empty)
            {
                _messagePrinter.PrintDoneMessage();
                return;
            }
            ViewAllTasks();
            var taskNumber = ChooseTaskNumber();
            MyToDoList.MarkAsComplete(taskNumber);
            _messagePrinter.PrintDoneMessage();
        }

        public void Delete()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }
            ViewAllTasks();
            var taskNumber = ChooseTaskNumber();
            MyToDoList.Remove(taskNumber);
            _messagePrinter.PrintDoneMessage();
        }
        public  void Exit()
        {
            DataSerializer.Serialize(MyToDoList);
            Environment.Exit(0);
        }

        public void ViewAllTasks()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }
            var table = FormATable();
            AnsiConsole.Render(table);
        }

        public IRenderable FormATable()
        {
            var table = new Table();
            table.AddColumn("No.");
            table.AddColumn(new TableColumn("Task").Centered());
            table.AddColumn(new TableColumn("State").Centered());

            for (int i = 0; i < MyToDoList.Count; i++)
            {
                var state = '\x2713';
             //   var state = Convert.ToChar("\x2713").ToString();
                if (MyToDoList[i].IsComplete)
                    // state = Convert.ToChar("\x2716").ToString();
                    state = '\x2716';
                table.AddRow(i.ToString(), MyToDoList[i].Description, state.ToString());
            }

            return table;
        }

        public int ChooseTaskNumber()
        {
            AnsiConsole.MarkupLine("[lightgoldenrod2_1]Choose the task number[/]");
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out var number) && number >= 0 && number < MyToDoList.Count)
                {
                    return number;
                }
                AnsiConsole.MarkupLine("[red]Incorrect number[/]");
                AnsiConsole.MarkupLine("Choose the task number");
            }
        }

        public void PrintErrorMessage()
        {
            _messagePrinter.PrintErrorMessage();
        }
    }
}
