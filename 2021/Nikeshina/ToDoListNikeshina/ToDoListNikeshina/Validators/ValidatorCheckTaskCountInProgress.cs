
namespace ToDoListNikeshina.Validators
{
    public class ValidatorCheckTaskCountInProgress : AbstractValidator
    {
        private ILogger logger;
        private ToDoList _toDo;
        private ValidatorTaskNumber indexValidator;
        private readonly StatusOfTask newStatus;

        public ValidatorCheckTaskCountInProgress(bool abort, ILogger logger, ToDoList list, ValidatorTaskNumber validator)
        {
            isAbortable = abort;
            this.logger = logger;
            _toDo = list;
            indexValidator = validator;
        }

        public ValidatorCheckTaskCountInProgress(bool abort, ToDoList list, ValidatorTaskNumber validator,StatusOfTask newStatus)
        {
            isAbortable = abort;
            _toDo = list;
            indexValidator = validator;
            this.newStatus = newStatus;
        }

        public override bool Validate()
        {
            bool result;
            int countInProgress = _toDo.GetCountTasksInProgress();
            int index = indexValidator.GetTaskNumber();

            if (logger != null)
                result = SwitchToNextStatus(countInProgress, index);
            else
                result = SwitchToUsersStatus(countInProgress, index);

            if (_nextValidator != null && ContinueCheck(result, isAbortable))
                result = (_nextValidator.Validate() && result);

            if (logger != null)
                PrintMessages(logger);
            return result; ;
        }
        private bool SwitchToUsersStatus(int progressingCount, int id)
        {
            if (progressingCount == 3 && _toDo[id].Status != StatusOfTask.InProgress && newStatus== StatusOfTask.InProgress)
            {
                return false;
            }
            else
                return true;
        }
        
        private bool SwitchToNextStatus(int countInProgress, int index)
        {
            if (countInProgress == 3 && _toDo[index].Status == StatusOfTask.Todo)
            {
                loggerMessages.Add(Messages.incorrectCountTasksInProgress);
                return false;
            }
            else
                return true;
        }
    }
}
