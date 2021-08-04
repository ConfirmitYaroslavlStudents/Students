using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleApp
    {
        private readonly ILogger _logger;
        public IGetterInputData DataGetter { get; private set; }
        private ToDoList List { get; set; }
        private readonly FileManager _fileManager;
        private readonly CommonOperationWithToDo _printer;
        private int _countTasksInProgress;

        private Stack<ToDoList> _lastLists = new Stack<ToDoList>();

        public ConsoleApp(ILogger logger,IGetterInputData dataGettter)
        {
            _logger = logger;
            DataGetter = dataGettter;
            _fileManager = new FileManager();
            List = new ToDoList(_fileManager.Load());
            _printer = new CommonOperationWithToDo(_logger, DataGetter, List);
            _countTasksInProgress = _fileManager.GetCountOfTaskInProgress();
        }

        public void Rollback()
        {
            _logger.Log(Messages.RequestNumberOfCommand());

            var inputStr = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputStr,_lastLists.Count))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            List = new ToDoList(GetLastList(num));
            _printer.UpdateToDo(List);

            _logger.Log(Messages.Completed());
        }

        private List<Task> GetLastList(int countOfStep)
        {
            if (countOfStep > _lastLists.Count)
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return List.GetListOfTask();
            }

            for (int i = 0; i < countOfStep - 1; i++)
                _lastLists.Pop();
            return _lastLists.Pop().GetListOfTask();
        }

        public void AddNewTask()
        {
            _logger.Log(Messages.RequestDescription());

            if(List.Count()!=0)
                 PushListToStack();
            if (!_printer.Add() && _lastLists.Count>0)
                _lastLists.Pop();
        }


        private void PushListToStack()
        {
            _lastLists.Push(new ToDoList(List.CopyList()));
        }

        public void Delete()
        {
            _logger.Log(Messages.RequestNumberOfString());
            PushListToStack();

            if (!_printer.Delete(_countTasksInProgress)&& _lastLists.Count > 0)
                _lastLists.Pop();
            else
                _countTasksInProgress = _printer.GetCountOfTaskInProgress();
        }

        public void EditDescription()
        {
            _logger.Log(Messages.RequestNumberOfString());

            var inputStr = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputStr, List.Count()))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);
            _logger.Log(Messages.RequestDescription());
            var dscr = DataGetter.GetInputData();

            if (!Validator.IsStringValid(dscr))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            PushListToStack();
            List.Edit(num, dscr);
            _logger.Log(Messages.Completed());
        }

        public void ChangeStatus()
        {
            _logger.Log(Messages.RequestNumberOfString());
            PushListToStack();

            if(!_printer.ChangeTaskStatus(_countTasksInProgress) &&_lastLists.Count > 0)
                _lastLists.Pop();
            else
                _countTasksInProgress = _printer.GetCountOfTaskInProgress();
        }

        public void Save() => _fileManager.Save(List._list);
        public void Print()
        {
            _printer.Print();
        }

        public List<Task> GetListOfTask() => List.GetListOfTask();
    }
}
