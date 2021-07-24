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

        public void ProcessOperation(string operationName)
        {
            switch (operationName)
            {
                case "add":
                    {
                        RunCommand(Add);
                        break;
                    }
                case "edit":
                {
                    RunCommand(Edit);
                    break;
                }
                case "complete":
                {
                    RunCommand(Complete);
                    break;
                }
                case "delete":
                {
                    RunCommand(Delete);
                    break;
                }
                case "list":
                {
                    RunCommand(List);
                    break;
                }
                case "exit":
                {
                    RunCommand(Exit);
                    break;
                }
                default:
                {
                    RunCommand(Error);
                    break;
                }
            }
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

        public void Add()
        {
            var description = _inputOutputManager.GetDescription();
            MyToDoList.Add(description);
            _inputOutputManager.PrintDoneMessage();
            List();
        }

        public void Edit()
        {
            var taskNumber = ChooseTaskNumber();
            _inputOutputManager.PrintNewDescriptionRequest();
            var newDescription = _inputOutputManager.ReadLine();
            MyToDoList.EditDescription(taskNumber, newDescription);
            _inputOutputManager.PrintDoneMessage();
            List();
        }

        public void Complete()
        {
            var taskNumber = ChooseTaskNumber();
            MyToDoList.Complete(taskNumber);
            _inputOutputManager.PrintDoneMessage();
            List();
        }

        public void Delete()
        {
            var taskNumber = ChooseTaskNumber();
                MyToDoList.Delete(taskNumber);
                _inputOutputManager.PrintDoneMessage();
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
            _inputOutputManager.RenderTable(table);
        }

        public void Error()
        {
            _errorPrinter.PrintErrorMessage();
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
