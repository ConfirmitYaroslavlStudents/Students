using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleApp : IApp
    {
        private readonly ILogger _logger;
        private readonly GeneralOperator _operator;
        private Stack<ToDoList> _pastListStates = new Stack<ToDoList>();

        public IGetInputData dataGetter;
        private ToDoList List { get; set; }

        public ConsoleApp(ILogger logger, IGetInputData dataGettter, List<Task> tasksFormFile)
        {
            _logger = logger;
            dataGetter = dataGettter;
            List = new ToDoList(tasksFormFile);
            _operator = new GeneralOperator(_logger, dataGetter, List);
            PushListToStack();
        }

        public void Rollback()
        {
            _logger.Log(Messages.requestNumberOfCommand);

            var inputString = dataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputString, _pastListStates.Count))
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return;
            }

            int inputNumber = int.Parse(inputString);

            List = new ToDoList(GetLastList(inputNumber));
            _operator.UpdateToDo(List);
            _logger.Log(Messages.completed);
        }

        private List<Task> GetLastList(int countOfStep)
        {
            if (countOfStep >= _pastListStates.Count)
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return List.GetListOfTask();
            }

            for (int i = 0; i < countOfStep; i++)
                _pastListStates.Pop();

            return _pastListStates.Pop().GetListOfTask();
        }

        public void AddNewTask()
        {
            _logger.Log(Messages.requestDescription);

            if (_operator.Add())
                PushListToStack();
        }


        private void PushListToStack()
        {
            _pastListStates.Push(new ToDoList(List.CopyList()));
        }

        public void Delete()
        {
            _logger.Log(Messages.requestNumberOfString);

            if (_operator.Delete())
                PushListToStack();
        }

        public void EditDescription()
        {
            _logger.Log(Messages.requestNumberOfString);

            var inputString = dataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputString, List.Count()))
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return;
            }

            int inputNoteNumber = int.Parse(inputString);
            _logger.Log(Messages.requestDescription);
            var newDescription = dataGetter.GetInputData();

            if (!Validator.IsStringValid(newDescription))
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return;
            }

            List.Edit(inputNoteNumber, newDescription);
            PushListToStack();
            _logger.Log(Messages.completed);
        }

        public void ChangeStatus()
        {
            _logger.Log(Messages.requestNumberOfString);

            if (_operator.ChangeTaskStatus())
                PushListToStack();
        }

        public void Print() => _operator.Print();

        public List<Task> GetListOfTask() => List.GetListOfTask();
    }
}
