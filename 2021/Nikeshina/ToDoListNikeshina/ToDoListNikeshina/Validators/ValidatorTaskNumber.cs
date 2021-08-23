using System.Collections.Generic;

namespace ToDoListNikeshina.Validators
{
    public class ValidatorTaskNumber : AbstractValidator
    {
        private IGetInputData dataResource;
        private List<Task> _list;
        private ILogger logger;
        private readonly int id;

        public ValidatorTaskNumber(bool abort, IGetInputData resource, ToDoList list, ILogger logger)
        {
            isAbortable = abort;
            _list = list.GetListOfTasks();
            dataResource = resource;
            this.logger = logger;
        }

        public ValidatorTaskNumber(bool abort, ToDoList list, int id)
        {
            _list = list.GetListOfTasks();
            this.id = id;
            isAbortable = abort;
        }

        public override bool Validate()
        {
            bool result;
            if (dataResource == null)
                result = ValidateWithInputParameters();
            else
                result = ValidateWithRequestData();


            if (_nextValidator != null && ContinueCheck(result, isAbortable))
                result = _nextValidator.Validate() && result;

            if (logger != null)
                PrintMessages(logger);
            return result;
        }

        private bool ValidateWithRequestData()
        {
            var inputNumber = dataResource.GetInputData();
            int index;
            if (int.TryParse(inputNumber, out index) && ListContainsId(index))
            {
                _taskNumber = index;
                return true;
            }
            else
            {
                loggerMessages.Add(Messages.incorrectTaskNumber);
                return false;
            }
        }

        private bool ValidateWithInputParameters()
        {
            if (ListContainsId(id))
            {
                _taskNumber = id;
                return true;
            }

            return false;
        }

        private bool ListContainsId(int id)
        {
            foreach (var task in _list)
                if (task._id == id)
                    return true;
            return false;
        }

    }
}
