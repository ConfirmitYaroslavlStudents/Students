using System;
using System.Collections.Generic;
using System.Text;
using ToDoListNikeshina.Validators;

namespace ToDoListNikeshina
{
    public class GeneralOperator
    {
        private readonly ILogger _logger;
        private readonly IGetInputData _dataGetter;
        private ToDoList _list;

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

        public bool Edit()
        {
            var numberVlidator = new ValidatorTaskNumber(_dataGetter, _list.Count(), _logger);
            var descriptionValidator = new CheckLengthDescription(_dataGetter, _logger);
            numberVlidator.SetNext(descriptionValidator);
            if (!numberVlidator.Validate())
                return false;

            _list.Edit(numberVlidator.GetTaskNumber(), descriptionValidator.GetDescription());
            _logger.Log(Messages.completed);
            return true;
        }

        public bool Delete()
        {
            var validator = new ValidatorTaskNumber(_dataGetter,_list.Count(), _logger);

            if (!validator.Validate())
                return false;

            int index = validator.GetTaskNumber();
            _list.Delete(index);
            _logger.Log(Messages.completed);
            return true;
        }

        public bool ChangeTaskStatus()
        {
            var numberValidator = new ValidatorTaskNumber(_dataGetter, _list.Count(), _logger);
            var countInProgresValidator = new ValidatorCheckTaskCountInProgress(_logger, _list, numberValidator);
            numberValidator.SetNext(countInProgresValidator);

            if (!numberValidator.Validate())
                return false;

            int index = numberValidator.GetTaskNumber();
            _list.ChangeStatus(index);
            _logger.Log(Messages.completed);
            return true;

        }

        public bool Add()
        {
            var validator = new CheckLengthDescription(_dataGetter, _logger);
            if (!validator.Validate())
                return false;

            var description = validator.GetDescription();
            _list.Add(new Task(description, StatusOfTask.Todo));
            _logger.Log(Messages.completed);
            return true;
        }

        public void UpdateToDo(ToDoList newItem) => _list = newItem;

        public List<Task> GetListOfTask() => _list.GetListOfTask();
    }
}
