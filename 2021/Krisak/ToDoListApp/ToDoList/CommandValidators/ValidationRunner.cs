using System.Collections.Generic;

namespace ToDoLibrary.CommandValidators
{
    public static class ValidationRunner
    {
        public static void Run(AbstractValidator validator)
        {
            validator.Validate();
            CheckForExceptions(validator.ExceptionMessages);
        }

        private static void CheckForExceptions(List<string> exceptions)
        {
            if (exceptions.Count!=0)
                 throw new WrongEnteredCommandException(string.Join('\n', exceptions));
        }
    }
}