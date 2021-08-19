using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class CheckLengthDescription : AbstractValidator
    {
        private IGetInputData dataResource;
        private ILogger logger;

        public CheckLengthDescription(bool abort, IGetInputData resource, ILogger logger)
        {
            isAbortable = abort;
            dataResource = resource;
            this.logger = logger;
        }

        public override bool Validate()
        {
            bool result;

            var inputDescription = dataResource.GetInputData();
            if (inputDescription.Length > 0 && inputDescription.Length <= 50)
            {
                _description = inputDescription;
                result = true;
            }
            else
            {
                loggerMessages.Add(Messages.incorrectDescriptionLength);
                result = false;
            }

            if (_nextValidator != null && ContinueCheck(result, isAbortable))
                result = _nextValidator.Validate() && result;

            PrintMessages(logger);
            return result;

        }
    }

}
