namespace ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput
{
    public class TryParseIntValidator: AbstractValidator
    {
        public TryParseIntValidator(bool isAbort) : base(isAbort) { }

        public override void Validate(string[] partsOfCommand)
        {
            if (!int.TryParse(partsOfCommand[1], out var result))
            {
                base.Exceptions.Add("Wrong index.");
                if (IsAbort)
                    return;
            }

            base.Validate(partsOfCommand);
        }
    }
}