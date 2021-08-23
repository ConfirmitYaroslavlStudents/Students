using System.Collections.Generic;

namespace ToDoListNikeshina
{
    public abstract class AbstractValidator 
    {
        protected AbstractValidator _nextValidator;
        protected int _taskNumber;
        protected string _description;
        protected int _actionsCount;
        protected bool isAbortable;
        protected List<string> loggerMessages = new List<string>();

        public int GetTaskNumber() => _taskNumber;

        public string GetDescription() => _description;

        public int GetActionsCount() => _actionsCount;

        public AbstractValidator SetNext(AbstractValidator validator)
        {
            _nextValidator = validator;
            return _nextValidator;
        }

        protected void PrintMessages(ILogger logger)
        {
            foreach (var message in loggerMessages)
                logger.Log(message);
        }

        protected bool ContinueCheck(bool itemResult, bool isAbort)
        {
            if (itemResult == false && isAbort == true)
                return false;
            return true;
        }
        public abstract bool Validate();
    }
}
