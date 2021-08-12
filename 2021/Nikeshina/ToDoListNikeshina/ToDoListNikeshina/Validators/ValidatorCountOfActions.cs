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

        public ValidatorCountOfActions(IGetInputData resource, int count, ILogger logger)
        {
            countOfActions = count;
            dataResource = resource;
            this.logger = logger;
        }
        public override bool Validate()
        {
            var inputNumber = dataResource.GetInputData();
            int count;
            if (int.TryParse(inputNumber, out count) && count > 0 && count <= countOfActions)
            {
                _actionsCount = count;
                if (_nextValidator != null)
                    return _nextValidator.Validate();
                else
                    return true;
            }
            else
                logger.Log(Messages.incorrectCommandsNumber);

            return false;
        }
    }
}
