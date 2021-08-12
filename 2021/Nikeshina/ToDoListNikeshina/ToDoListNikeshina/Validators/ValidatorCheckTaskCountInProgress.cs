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
        public ValidatorCheckTaskCountInProgress(ILogger logger, ToDoList list, ValidatorTaskNumber validator)
        {
            this.logger = logger;
            _toDo = list;
            indexValidator = validator;
        }

        public override bool Validate()
        {
            int index = indexValidator.GetTaskNumber();
            int countInProgress = _toDo.GetCountTasksInProgress();
            if (countInProgress == 3 && _toDo[index - 1].Status == StatusOfTask.Todo)
                logger.Log(Messages.incorrectCountTasksInProgress);
            else
            {
                if (_nextValidator != null)
                    return _nextValidator.Validate();
                else
                    return true;
            }

            return false;
        }
    }
}
