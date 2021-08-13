using System.Collections.Generic;
using System.Net.NetworkInformation;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public abstract class AbstractValidator
    {
        private AbstractValidator _next;
        protected readonly bool IsAbort;
        public List<string> Exceptions { get; protected set; } = new List<string>();

        public AbstractValidator (bool isAbort)
        {
            IsAbort = isAbort;
        }

        public AbstractValidator SetNext(AbstractValidator validator)
        {
            validator.Exceptions = Exceptions;
            _next = validator;
            return validator;
        }
        public virtual void Validate(ICommand command)
        {
            _next?.Validate(command);
        }

        public virtual void Validate(string[] partsOfCommand)
        {
            _next?.Validate(partsOfCommand);
        }
    }
}