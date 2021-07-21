using System;
using MyToDoList;

namespace ToDoApp
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

        public void RunCommand(Action command)
        {
            command();
            _messagePrinter.PrintDoneMessage();
            ViewAllTasks();
            DataHandler.SaveToFile(MyToDoList);
        }

        public void Add()
        {
            RunCommand(() =>
            {
                var description = _messagePrinter.GetDescription();
                MyToDoList.Add(description);
            });
        }

        public void Edit()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }

            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                _messagePrinter.PrintNewDescriptionRequest();
                var newDescription = _messagePrinter.ReadLine();
                MyToDoList.EditDescription(taskNumber, newDescription);
            });
        }

        public void MarkAsComplete()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }
            
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.MarkAsComplete(taskNumber);
            });
        }

        public void Delete()
        {
            if (Empty)
            {
                _messagePrinter.PrintErrorMessage();
                return;
            }
            RunCommand(() =>
            {
                var taskNumber = ChooseTaskNumber();
                MyToDoList.Delete(taskNumber);
            });
        }

        public  void Exit()
        {
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
            _messagePrinter.RenderTable(table);
        }

        public int ChooseTaskNumber()
        {
            _messagePrinter.PrintTaskNumberRequest();
            var input = _messagePrinter.ReadLine();
            return int.Parse(input);
        }
        
        public string GetMenuItemName() => _messagePrinter.GetMenuItemName();
    }
}
