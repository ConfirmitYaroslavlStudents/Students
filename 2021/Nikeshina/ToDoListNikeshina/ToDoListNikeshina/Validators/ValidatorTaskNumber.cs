using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public class ValidatorTaskNumber : AbstractValidator
    {
        private IGetInputData dataResource;
        private int listCount;
        private ILogger logger;
        
        public ValidatorTaskNumber(bool abort, IGetInputData resource, int count, ILogger logger)
        {
            isAbortable = abort;
            listCount = count;
            dataResource = resource;
            this.logger = logger;
        }
        public override bool Validate()
        {
            bool result;
            var inputNumber = dataResource.GetInputData();
            int index;
            if (int.TryParse(inputNumber, out index) && index > 0 && index <= listCount)
            {
                _taskNumber = index;
                result = true;
            }
            else
            {
                loggerMessages.Add(Messages.incorrectTaskNumber);
                result = false;
            }

            if (_nextValidator != null && ContinueCheck(result,isAbortable))
                result = _nextValidator.Validate() && result;

            PrintMessages(logger);
            return result;
        }
        
    }
}
