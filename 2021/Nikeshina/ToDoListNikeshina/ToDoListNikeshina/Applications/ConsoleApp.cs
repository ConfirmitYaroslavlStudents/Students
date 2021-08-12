using System;
using System.Collections.Generic;
using System.Text;
using ToDoListNikeshina.Validators;

namespace ToDoListNikeshina
{
    public class ConsoleApp : IApp
    {
        private readonly ILogger _logger;
        private readonly GeneralOperator _operator;
        private Stack<ToDoList> _pastLStates = new Stack<ToDoList>();

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
            var validator = new ValidatorCountOfActions(dataGetter,_pastLStates.Count, _logger);
            if (!validator.Validate())
                return;

            List = new ToDoList(GetLastList(validator.GetActionsCount()));
            _operator.UpdateToDo(List);
            _logger.Log(Messages.completed);
        }

        private List<Task> GetLastList(int countOfStep)
        {
            for (int i = 0; i < countOfStep; i++)
                _pastLStates.Pop();

            return _pastLStates.Peek().GetListOfTask();
        }

        public void AddNewTask()
        {
            _logger.Log(Messages.requestDescription);

            if (_operator.Add())
                PushListToStack();
        }


        private void PushListToStack()
        {
            _pastLStates.Push(new ToDoList(List.CopyList()));
        }

        public void Delete()
        {
            _logger.Log(Messages.requestNumberOfString);

            if (_operator.Delete())
                PushListToStack();
        }

        public void EditDescription()
        {
            _logger.Log(Messages.requestNumberAndDescription);

            if(_operator.Edit())
                PushListToStack();
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
