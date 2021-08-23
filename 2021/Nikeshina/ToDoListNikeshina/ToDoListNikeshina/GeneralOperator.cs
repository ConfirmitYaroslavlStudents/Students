using System.Collections.Generic;
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
                foreach (var task in GetListOfTask())
                    _logger.Log(task.StringFormat());
        }

        public bool Edit()
        {
            var numberVlidator = new ValidatorTaskNumber(true,_dataGetter, _list, _logger);
            var descriptionValidator = new CheckLengthDescription(false, _dataGetter, _logger);
            numberVlidator.SetNext(descriptionValidator);
            if (!numberVlidator.Validate())
                return false;

            _list.Edit(numberVlidator.GetTaskNumber(), descriptionValidator.GetDescription());
            _logger.Log(Messages.completed);
            return true;
        }

        public bool Delete()
        {
            var validator = new ValidatorTaskNumber(true, _dataGetter,_list, _logger);

            if (!validator.Validate())
                return false;

            int index = validator.GetTaskNumber();
            _list.Delete(index);
            _logger.Log(Messages.completed);
            return true;
        }

        public bool ChangeTaskStatus()
        {
            var numberValidator = new ValidatorTaskNumber(true, _dataGetter, _list, _logger);
            var countInProgresValidator = new ValidatorCheckTaskCountInProgress(true, _logger, _list, numberValidator);
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
            var validator = new CheckLengthDescription(false, _dataGetter, _logger);
            if (!validator.Validate())
                return false;

            var description = validator.GetDescription();
            _list.Add(description, StatusOfTask.Todo);
            _logger.Log(Messages.completed);
            return true;
        }

        public void UpdateToDo(ToDoList newItem) => _list = newItem;

        public List<Task> GetListOfTask() => _list.GetListOfTasks();
    }
}
