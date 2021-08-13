using System;
using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public static class ValidatorRunner
    {
        public static void Run(AbstractValidator validator, ICommand command)
        {
            validator.Validate(command);
            CheckForExceptions(validator.Exceptions);
        }

        public static void Run(AbstractValidator validator, string[] partsOfCommand)
        {
            validator.Validate(partsOfCommand);
            CheckForExceptions(validator.Exceptions);
        }

        private static void CheckForExceptions(List<string> exceptions)
        {
            if (exceptions.Count!=0)
                throw new WrongEnteredCommandException(string.Join('\n', exceptions));
        }
    }
}