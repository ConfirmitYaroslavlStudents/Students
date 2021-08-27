namespace ToDoLibrary.CommandValidators.ValidatorForUserInput
{
    public class IntTryParseValidator: AbstractValidator
    {
        private readonly string _index;

        public IntTryParseValidator(bool isAbort, string index) : base(isAbort)
            => _index = index;

        public override void Validate()
        {
            if (!int.TryParse(_index, out var result))
            {
                ExceptionMessages.Add("Wrong index.");
                if (IsAbort)
                    return;
            }

            base.Validate();
        }
    }
}