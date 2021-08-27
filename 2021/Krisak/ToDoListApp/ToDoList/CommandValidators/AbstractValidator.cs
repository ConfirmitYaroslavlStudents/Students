using System.Collections.Generic;

namespace ToDoLibrary.CommandValidators
{
    public abstract class AbstractValidator
    {
        private AbstractValidator _next;
        protected readonly bool IsAbort;
        public List<string> ExceptionMessages { get; protected set; } = new List<string>();

        public AbstractValidator (bool isAbort)
        {
            IsAbort = isAbort;
        }

        public AbstractValidator SetNext(AbstractValidator validator)
        {
            validator.ExceptionMessages = ExceptionMessages;
            _next = validator;
            return validator;
        }

        public virtual void Validate()
        {
            _next?.Validate();
        }
    }
}