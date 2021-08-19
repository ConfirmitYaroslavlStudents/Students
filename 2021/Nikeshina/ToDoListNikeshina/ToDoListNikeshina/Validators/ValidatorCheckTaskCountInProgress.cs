using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class ValidatorCheckTaskCountInProgress : AbstractValidator
    {
        private ILogger logger;
        private ToDoList _toDo;
        private ValidatorTaskNumber indexValidator;
        public ValidatorCheckTaskCountInProgress(bool abort, ILogger logger, ToDoList list, ValidatorTaskNumber validator)
        {
            isAbortable = abort;
            this.logger = logger;
            _toDo = list;
            indexValidator = validator;
        }

        public override bool Validate()
        {
            bool result;
            int index = indexValidator.GetTaskNumber();
            int countInProgress = _toDo.GetCountTasksInProgress();
            if (countInProgress == 3 && _toDo[index - 1].Status == StatusOfTask.Todo)
            {
                loggerMessages.Add(Messages.incorrectCountTasksInProgress);
                result = false;
            }
            else
                result = true;

            if (_nextValidator != null && ContinueCheck(result, isAbortable))
                result = (_nextValidator.Validate() && result);

            PrintMessages(logger);
            return result; ;
        }
    }
}
