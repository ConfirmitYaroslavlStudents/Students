using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class ValidatorCountOfActions : AbstractValidator
    {
        protected IGetInputData dataResource;
        protected int countOfActions;
        private ILogger logger;

        public ValidatorCountOfActions(bool abort, IGetInputData resource, int count, ILogger logger)
        {
            isAbortable = abort;
            countOfActions = count;
            dataResource = resource;
            this.logger = logger;
        }
        public override bool Validate()
        {
            bool result;
            var inputNumber = dataResource.GetInputData();
            int count;
            if (int.TryParse(inputNumber, out count) && count > 0 && count <= countOfActions)
            {
                _actionsCount = count;
                result = true;
            }
            else
            {
                loggerMessages.Add(Messages.incorrectCommandsNumber);
                return false;
            }

            if (_nextValidator != null && ContinueCheck(result,isAbortable))
                result = _nextValidator.Validate() && result;

            PrintMessages(logger);
            return result;
        }
    }
}
