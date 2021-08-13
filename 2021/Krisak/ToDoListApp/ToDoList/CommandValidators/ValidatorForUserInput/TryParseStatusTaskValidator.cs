using System;

namespace ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput
{
    public class TryParseStatusTaskValidator : AbstractValidator
    {
        public TryParseStatusTaskValidator(bool isAbort) : base(isAbort) { }

        public override void Validate(string[] partsOfCommand)
        {
            if (!int.TryParse(partsOfCommand[2], out int result) || result < 0 || result > 2)
            {
                base.Exceptions.Add("Wrong status number.");

                if (IsAbort)
                    return;
            }

            base.Validate(partsOfCommand);
        }
    }
}