using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class ValidatorTaskNumber : AbstractValidator
    {
        protected IGetInputData dataResource;
        protected int listCount;
        private ILogger logger;
        public ValidatorTaskNumber(IGetInputData resource, int count, ILogger logger)
        {
            listCount = count;
            dataResource = resource;
            this.logger = logger;
        }
        public override bool Validate()
        {
            var inputNumber = dataResource.GetInputData();
            int index;
            if (int.TryParse(inputNumber, out index) && index > 0 && index <= listCount)
            {
                _taskNumber = index;
                if (_nextValidator != null)
                    return _nextValidator.Validate();
                else
                    return true;
            }
            else
                logger.Log(Messages.incorrectTaskNumber);

            return false;
        }
    }
}
