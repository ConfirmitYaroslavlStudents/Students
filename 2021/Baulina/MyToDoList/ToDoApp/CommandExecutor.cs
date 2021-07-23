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
                _inputOutputManager.PrintDoneMessage();
                List();
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

        public void Add()
        {
            RunCommand(() =>
            {
                var description = _inputOutputManager.GetDescription();
                MyToDoList.Add(description);
            });
        }

        public void Edit()
        {
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                _inputOutputManager.PrintNewDescriptionRequest();
                var newDescription = _inputOutputManager.ReadLine();
                MyToDoList.EditDescription(taskNumber, newDescription);
            });
        }

        public void Complete()
        {
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.Complete(taskNumber);
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

        public void List()
        {
            if (Empty)
            {
                _errorPrinter.PrintErrorMessage();
                return;
            }

            var tableBuilder = new TableBuilder(MyToDoList);
            var table = tableBuilder.FormATable();
            _inputOutputManager.RenderTable(table);
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
