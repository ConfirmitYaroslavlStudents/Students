using System.Collections.Generic;
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

        public ConsoleApp(ILogger logger, IGetInputData dataGettter, List<Task> tasksFormFile, int idCount)
        {
            _logger = logger;
            dataGetter = dataGettter;
            List = new ToDoList(tasksFormFile,idCount);
            _operator = new GeneralOperator(_logger, dataGetter, List);
            PushListToStack();
        }

        public void Rollback()
        {
            _logger.Log(Messages.requestNumberOfCommand);
            var validator = new ValidatorCountOfActions(true, dataGetter,_pastLStates.Count, _logger);
            if (!validator.Validate())
                return;
            
            List = GetLastList(validator.GetActionsCount());
            _operator.UpdateToDo(List);
            _logger.Log(Messages.completed);
        }

        private ToDoList GetLastList(int countOfStep)
        {
            for (int i = 0; i < countOfStep; i++)
                _pastLStates.Pop();

            return _pastLStates.Peek();
        }

        public void AddNewTask()
        {
            _logger.Log(Messages.requestDescription);

            if (_operator.Add())
                PushListToStack();
        }


        private void PushListToStack()
        {
            _pastLStates.Push(List.CopyList());
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

        public List<Task> GetListOfTask() => List.GetListOfTasks();
    }
}
