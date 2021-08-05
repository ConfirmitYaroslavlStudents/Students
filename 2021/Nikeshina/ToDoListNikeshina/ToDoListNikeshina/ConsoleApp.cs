using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleApp
    {
        private readonly ILogger _logger;
        private readonly FileManager _fileManager;
        private readonly GeneralOperator _operator;
        private int _countTasksInProgress;
        private Stack<ToDoList> _pastListStates = new Stack<ToDoList>();

        public IGetInputData DataGetter { get; private set; }
        private ToDoList List { get; set; }
       
        public ConsoleApp(ILogger logger,IGetInputData dataGettter)
        {
            _logger = logger;
            DataGetter = dataGettter;
            _fileManager = new FileManager();
            List = new ToDoList(_fileManager.Load());
            _operator = new GeneralOperator(_logger, DataGetter, List);
            _countTasksInProgress = _fileManager.GetCountOfTaskInProgress();
        }

        public void Rollback()
        {
            _logger.Log(Messages.RequestNumberOfCommand());

            var inputString = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputString,_pastListStates.Count))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int inputNumber = int.Parse(inputString);

            List = new ToDoList(GetLastList(inputNumber));
            _operator.UpdateToDo(List);
            _logger.Log(Messages.Completed());
        }

        private List<Task> GetLastList(int countOfStep)
        {
            if (countOfStep > _pastListStates.Count)
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return List.GetListOfTask();
            }

            for (int i = 0; i < countOfStep - 1; i++)
                _pastListStates.Pop();

            return _pastListStates.Pop().GetListOfTask();
        }

        public void AddNewTask()
        {
            _logger.Log(Messages.RequestDescription());

            if(List.Count()!=0 || (List.Count()==0 && _pastListStates.Count!=0))
                 PushListToStack();
            if (!_operator.Add() && _pastListStates.Count>0)
                _pastListStates.Pop();
        }


        private void PushListToStack()
        {
            _pastListStates.Push(new ToDoList(List.CopyList()));
        }

        public void Delete()
        {
            _logger.Log(Messages.RequestNumberOfString());
            PushListToStack();

            if (!_operator.Delete(_countTasksInProgress)&& _pastListStates.Count > 0)
                _pastListStates.Pop();
            else
                _countTasksInProgress = _operator.GetCountOfTaskInProgress();
        }

        public void EditDescription()
        {
            _logger.Log(Messages.RequestNumberOfString());

            var inputString = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputString, List.Count()))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int inputNoteNumber = int.Parse(inputString);
            _logger.Log(Messages.RequestDescription());
            var newDescription = DataGetter.GetInputData();

            if (!Validator.IsStringValid(newDescription))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            PushListToStack();
            List.Edit(inputNoteNumber, newDescription);
            _logger.Log(Messages.Completed());
        }

        public void ChangeStatus()
        {
            _logger.Log(Messages.RequestNumberOfString());

            PushListToStack();

            if(!_operator.ChangeTaskStatus(_countTasksInProgress) &&_pastListStates.Count > 0)
                _pastListStates.Pop();
            else
                _countTasksInProgress = _operator.GetCountOfTaskInProgress();
        }

        public void Save() => _fileManager.Save(List._list);
        public void Print()
        {
            _operator.Print();
        }

        public List<Task> GetListOfTask() => List.GetListOfTask();
    }
}
