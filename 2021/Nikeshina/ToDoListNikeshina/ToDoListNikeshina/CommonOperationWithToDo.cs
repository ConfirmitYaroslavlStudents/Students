using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CommonOperationWithToDo
    {
        private readonly ILogger _logger;
        private readonly IGetterInputData _dataGetter;
        private ToDoList _list;
        private int _countOfTaskInpro;

        public CommonOperationWithToDo(ILogger logger, IGetterInputData dataGetter, ToDoList list)
        {
            _logger = logger;
            _dataGetter = dataGetter;
            _list = list;
        }
        public void Print()
        {
            if (_list.Count() == 0)
                _logger.Log(Messages.ListIsEmpty());
            else
            {
                int i = 1;
                foreach (var task in _list._list)
                {
                    _logger.Log(i + ". " + task.PrintTask());
                    i++;
                }
            }
        }
        public bool Delete(int taskCountInProgress)
        {
            return DoCommandWithRequestNumber(taskCountInProgress,_list.Delete, DecrementCountTasksInProgres);
        }

        public bool ChangeTaskStatus(int taskCountInProgress)
        {
            return DoCommandWithRequestNumber(taskCountInProgress,_list.ChangeStatus, IncrementCountTasksInProgres);
        }

        private bool DoCommandWithRequestNumber(int countTasksInProgressBefore,Action<int> comand, Action<int> checkCountTaskInProgress)
        {
            var inputStr = _dataGetter.GetInputData();
            _countOfTaskInpro = countTasksInProgressBefore;


            if (!Validator.IsNumberValid(inputStr, _list.Count()))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return false;
            }
            
            int num = int.Parse(inputStr);

                
                checkCountTaskInProgress(num-1);
                if (_countOfTaskInpro > 3)

                {
                    
                    _logger.Log(Messages.WrongFormatOfInputData());
                     return false;
                }
                comand(num);
                _logger.Log(Messages.Completed());

            return true;
        }

        private void IncrementCountTasksInProgres(int index)
        {
            if(_list._list[index].Status==0)
                _countOfTaskInpro++;
            else if (_list._list[index].Status== StatusOfTask.InProgress)
                _countOfTaskInpro--;
        }

        private void DecrementCountTasksInProgres(int index)
        {
            if (_list._list[index].Status==StatusOfTask.InProgress)
                _countOfTaskInpro--;
        }

        public bool Add()
        {
            var dscr = _dataGetter.GetInputData();

            if (!Validator.IsStringValid(dscr))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return false;
            }

            _list.Add(new Task(dscr, 0));
            _logger.Log(Messages.Completed());
            return true;
        }
        public void UpdateToDo(ToDoList newItem) => _list = newItem;
        public List<Task> GetListOfTask() => _list.GetListOfTask();

        public int GetCountOfTaskInProgress() => _countOfTaskInpro;
    }
}
