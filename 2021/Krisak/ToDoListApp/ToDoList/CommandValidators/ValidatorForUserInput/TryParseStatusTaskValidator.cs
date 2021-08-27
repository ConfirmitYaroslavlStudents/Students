namespace ToDoLibrary.CommandValidators.ValidatorForUserInput
{
    public class TryParseStatusTaskValidator : AbstractValidator
    {
        private readonly string _status;

        public TryParseStatusTaskValidator(bool isAbort, string status) : base(isAbort)
            => _status = status;

        public override void Validate()
        {
            var intStatus = int.Parse(_status);
            if (intStatus < 0 || intStatus > 2)
            {
                ExceptionMessages.Add("Wrong status number.");

                if (IsAbort)
                    return;
            }

            base.Validate();
        }
    }
}