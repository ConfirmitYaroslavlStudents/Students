using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class GeneralOperator
    {
        private readonly ILogger _logger;
        private readonly IGetInputData _dataGetter;
        private ToDoList _list;
        private int count;

        public GeneralOperator(ILogger logger, IGetInputData dataGetter, ToDoList list)
        {
            _logger = logger;
            _dataGetter = dataGetter;
            _list = list;
        }
        public void Print()
        {
            if (_list.Count() == 0)
                _logger.Log(Messages.listIsEmpty);
            else
            {
                int i = 1;
                foreach (var task in GetListOfTask())
                {
                    _logger.Log(i + ". " + task.StringFormat());
                    i++;
                }
            }
        }
        public bool Delete()
        {
            return DoCommandWithRequestNumber(_list.Delete, ChangeCountTaskInProgressDuringDelete);
        }

        public bool ChangeTaskStatus()
        {
            return DoCommandWithRequestNumber(_list.ChangeStatus,ChangeCountTaskInProgressDuringChangeStatus);
        }

        private bool DoCommandWithRequestNumber(Action<int> comand, Action<int> checkCountInProgress)
        {
            var inputStr = _dataGetter.GetInputData();


            if (!Validator.IsNumberValid(inputStr, _list.Count()))
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return false;
            }

            int num = int.Parse(inputStr);
            checkCountInProgress(num);


            if (count>3)
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return false;
            }

            comand(num);
            _logger.Log(Messages.completed);

            return true;
        }

        private void ChangeCountTaskInProgressDuringDelete(int index)
        {
            count = _list.GetCountTasksInProgress();
            if (_list[index - 1].Status == StatusOfTask.InProgress)
                count--;
        }

        private void ChangeCountTaskInProgressDuringChangeStatus(int index)
        {
            count = _list.GetCountTasksInProgress();
            if (_list[index - 1].Status == StatusOfTask.InProgress)
                count--;
            else if (_list[index - 1].Status == StatusOfTask.Todo)
                count++;
        }

        public bool Add()
        {
            var dscr = _dataGetter.GetInputData();

            if (!Validator.IsStringValid(dscr))
            {
                _logger.Log(Messages.wrongFormatOfInputData);
                return false;
            }

            _list.Add(new Task(dscr, StatusOfTask.Todo));
            _logger.Log(Messages.completed);
            return true;
        }
        public void UpdateToDo(ToDoList newItem) => _list = newItem;
        public List<Task> GetListOfTask() => _list.GetListOfTask();
    }
}
