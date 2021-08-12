using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class CheckLengthDescription : AbstractValidator
    {
        protected IGetInputData dataResource;
        protected ILogger logger;

        public CheckLengthDescription(IGetInputData resource, ILogger logger)
        {
            dataResource = resource;
            this.logger = logger;
        }

        public override bool Validate()
        {
            var inputDescription = dataResource.GetInputData();
            if (inputDescription.Length > 0 && inputDescription.Length <= 50)
            {
                _description = inputDescription;
                if (_nextValidator != null)
                    return _nextValidator.Validate();
                else
                    return true;
            }
            else
                logger.Log(Messages.incorrectDescriptionLength);

            return false;
        }
    }

}
