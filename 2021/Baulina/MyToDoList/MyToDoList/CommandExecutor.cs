using System;
using MyToDoList;
using ConsoleInteractors;

namespace ToDoApp
{
    public class CommandExecutor : IExecute
    {
        public ToDoList MyToDoList { get; }
        private readonly ConsoleHandler _consoleHandler;
        private readonly ErrorPrinter _errorPrinter;
        internal bool Empty => MyToDoList.Count == 0;
        public bool IsWorking { get; private set; }

        public CommandExecutor(ToDoList inputList, ConsoleHandler printer)
        {
            MyToDoList = new ToDoList(inputList);
            _consoleHandler = printer;
            IsWorking = true;
            _errorPrinter = new ErrorPrinter(printer);
        }

        public void RunCommand(Action command)
        {
            try
            {
                command();
                _consoleHandler.PrintDoneMessage();
                ViewAllTasks();
                DataHandler.SaveToFile(MyToDoList);

            }
            catch (ArgumentOutOfRangeException)
            {
                _errorPrinter.PrintIncorrectNumberWarning();
            }
            catch (Exception)
            {
                _errorPrinter.PrintErrorMessage();
            }
        }

        public void Add()
        {
            RunCommand(() =>
            {
                var description = _consoleHandler.GetDescription();
                MyToDoList.Add(description);
            });
        }

        public void Edit()
        {
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                _consoleHandler.PrintNewDescriptionRequest();
                var newDescription = _consoleHandler.ReadLine();
                MyToDoList.EditDescription(taskNumber, newDescription);
            });
        }

        public void MarkAsComplete()
        {
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.MarkAsComplete(taskNumber);
            });
        }

        public void Delete()
        {
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.Delete(taskNumber);
            });
        }

        public void Exit()
        {
            IsWorking = false;
        }

        public void ViewAllTasks()
        {
            if (Empty)
            {
                _errorPrinter.PrintErrorMessage();
                return;
            }

            var tableBuilder = new TableBuilder(MyToDoList);
            var table = tableBuilder.FormATable();
            _consoleHandler.RenderTable(table);
        }

        public int ChooseTaskNumber()
        {
            _consoleHandler.PrintTaskNumberRequest();
            var input = _consoleHandler.ReadLine();
            var result = int.Parse(input);
            if (result < 0 || result >= MyToDoList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
