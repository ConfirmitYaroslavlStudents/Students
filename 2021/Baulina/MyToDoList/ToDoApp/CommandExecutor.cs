using System;
using MyToDoList;
using InputOutputManagers;

namespace ToDoApp
{
    public class CommandExecutor : IExecute
    {
        public ToDoList MyToDoList { get; }
        private readonly IConsoleExtended _console;
        internal bool Empty => MyToDoList.Count == 0;
        public bool IsWorking { get; private set; }

        public CommandExecutor(ToDoList inputList, IConsoleExtended console)
        {
            MyToDoList = new ToDoList(inputList);
            _console = console;
            IsWorking = true;
        }

        public Action GetCommand(string operationName)
        {
            return operationName switch
            {
                "add" => Add,
                "edit" => Edit,
                "complete" => Complete,
                "delete" => Delete,
                "list" => List,
                "exit" => Exit,
                _ => Error
            };
        }

        public void ProcessOperation(string operationName)
        {
            var command = GetCommand(operationName);
            RunCommand(command);
        }

        public void RunCommand(Action command)
        {
            try
            {
                command();
                new FileSaveAndLoad().SaveTheList(MyToDoList);

            }
            catch (InvalidOperationException)
            {
                _console.PrintIncorrectNumberWarning();
            }
            catch (Exception)
            {
                _console.PrintErrorMessage();
            }
        }

        public void Add()
        {
            var description = _console.GetDescription();
            MyToDoList.Add(description);
            _console.PrintDoneMessage();
            List();
        }

        public void Edit()
        {
            var taskNumber = ChooseTaskNumber();
            _console.PrintNewDescriptionRequest();
            var newDescription = _console.ReadLine();
            MyToDoList.EditDescription(taskNumber, newDescription);
            _console.PrintDoneMessage();
            List();
        }

        public void Complete()
        {
            var taskNumber = ChooseTaskNumber();
            MyToDoList.Complete(taskNumber);
            _console.PrintDoneMessage();
            List();
        }

        public void Delete()
        {
            var taskNumber = ChooseTaskNumber();
                MyToDoList.Delete(taskNumber);
                _console.PrintDoneMessage();
            List();
        }

        public void Exit()
        {
            IsWorking = false;
        }

        public void List()
        {
            var tableBuilder = new TableBuilder(MyToDoList);
            var table = tableBuilder.FormATable();
            _console.RenderTable(table);
        }

        public void Error()
        {
            _console.PrintErrorMessage();
        }

        public int ChooseTaskNumber()
        {
            _console.PrintTaskNumberRequest();
            var input = _console.ReadLine();
            var result = int.Parse(input);
            if (result < 0 || result >= MyToDoList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
