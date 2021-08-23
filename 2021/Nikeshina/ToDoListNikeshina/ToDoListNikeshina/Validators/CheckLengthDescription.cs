
namespace ToDoListNikeshina.Validators
{
    public class CheckLengthDescription : AbstractValidator
    {
        private IGetInputData dataResource;
        private ILogger logger;
        private readonly string description;

        public CheckLengthDescription(bool abort, IGetInputData resource, ILogger logger)
        {
            isAbortable = abort;
            dataResource = resource;
            this.logger = logger;
        }

        public CheckLengthDescription(bool abort, string description)
        {
            isAbortable = abort;
            this.description = description;
        }

        public override bool Validate()
        {
            bool result;

            if (dataResource == null)
                result = ValidateWithInputParameters();
            else
                result = ValidateWithRequestData();

            if (_nextValidator != null && ContinueCheck(result, isAbortable))
                result = _nextValidator.Validate() && result;

            if (logger != null)
                PrintMessages(logger);
            return result;

        }

        private bool ValidateWithRequestData()
        {
            var inputDescription = dataResource.GetInputData();
            if (inputDescription.Length > 0 && inputDescription.Length <= 50)
            {
                _description = inputDescription;
                return true;
            }
            else
            {
                loggerMessages.Add(Messages.incorrectDescriptionLength);
                return false;
            }
        }

        private bool ValidateWithInputParameters()
        {
            if (description.Length > 0 && description.Length <= 50)
            {
                _description = description;
                return true;
            }
            return false;
        }
    }

}
