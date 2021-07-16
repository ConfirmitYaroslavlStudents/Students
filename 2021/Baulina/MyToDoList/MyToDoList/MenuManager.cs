using System;
using MyToDoList;
using Spectre.Console;

namespace ToDoListConsole
{
    public class MenuManager : IManage
    {
        public ToDoList MyToDoList { get; }
        private readonly MessagePrinter _messagePrinter;

        internal bool Empty => MyToDoList.Count == 0;
        public bool IsWorking { get; private set; }

        public MenuManager(ToDoList inputList, MessagePrinter printer)
        {
            MyToDoList = new ToDoList(inputList);
            _messagePrinter = printer;
            IsWorking = true;
        }

        public void Add()
        {
            var description = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold lightgoldenrod2_1]What do you need to do? [/]")
                    .Validate(task =>
                    {
                        return task switch
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
            _messagePrinter.PrintNewDescriptionRequest();
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
            MyToDoList.Delete(taskNumber);
            _messagePrinter.PrintDoneMessage();
        }

        public  void Exit()
        {
            DataHandler.SaveToFile(MyToDoList);
            IsWorking = false;
        }

        public void ViewAllTasks()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }

            var tableBuilder = new TableBuilder(MyToDoList);
            var table = tableBuilder.FormATable();
            AnsiConsole.Render(table);
        }

        public int ChooseTaskNumber()
        {
            _messagePrinter.PrintTaskNumberRequest();
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out var number) && number >= 0 && number < MyToDoList.Count)
                {
                    return number;
                }
                _messagePrinter.PrintIncorrectNumberWarning();
                _messagePrinter.PrintTaskNumberRequest();
            }
        }

        public void PrintErrorMessage()
        {
            _messagePrinter.PrintErrorMessage();
        }
    }
}
