using System;
using MyToDoList;
using InputOutputManagers;

namespace ToDoApp
{
    public class CommandExecutor : IExecute
    {
        public ToDoList MyToDoList { get; }
        private readonly InputOutputManager _inputOutputManager;
        private readonly ErrorPrinter _errorPrinter = new(new ConsoleInteractor());
        internal bool Empty => MyToDoList.Count == 0;
        public bool IsWorking { get; private set; }

        public CommandExecutor(ToDoList inputList, InputOutputManager inputOutputManager)
        {
            MyToDoList = new ToDoList(inputList);
            _inputOutputManager = inputOutputManager;
            IsWorking = true;
        }

        public CommandExecutor(ToDoList inputList, InputOutputManager inputOutputManager, ErrorPrinter errorPrinter) :
            this(inputList, inputOutputManager)
        {
            _errorPrinter = errorPrinter;
        }

        public void RunCommand(Action command)
        {
            try
            {
                command();
                new FileManager().SaveToFile(MyToDoList);

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

        public Action Add()
        {
            return () =>
            {
                var description = _inputOutputManager.GetDescription();
                MyToDoList.Add(description);
                _inputOutputManager.PrintDoneMessage();
                RunCommand(List());
            };
        }

        public Action Edit()
        {
            return () =>
            {
                var taskNumber = ChooseTaskNumber();
                _inputOutputManager.PrintNewDescriptionRequest();
                var newDescription = _inputOutputManager.ReadLine();
                MyToDoList.EditDescription(taskNumber, newDescription);
                _inputOutputManager.PrintDoneMessage();
                RunCommand(List());
            };
        }

        public Action Complete()
        {
            return () =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.Complete(taskNumber);
                _inputOutputManager.PrintDoneMessage();
                RunCommand(List());
            };
        }

        public Action Delete()
        {
            return () =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.Delete(taskNumber);
                _inputOutputManager.PrintDoneMessage();
                RunCommand(List());
            };
        }

        public Action Exit()
        {
            return () =>
            {
                IsWorking = false;
            };
        }

        public Action List()
        {
            return () =>
            {
                var tableBuilder = new TableBuilder(MyToDoList);
                var table = tableBuilder.FormATable();
                _inputOutputManager.RenderTable(table);
            };
        }

        public Action Error()
        {
            return () => _errorPrinter.PrintErrorMessage();
        }

        public int ChooseTaskNumber()
        {
            _inputOutputManager.PrintTaskNumberRequest();
            var input = _inputOutputManager.ReadLine();
            var result = int.Parse(input);
            if (result < 0 || result >= MyToDoList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
