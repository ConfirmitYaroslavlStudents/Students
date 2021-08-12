using System;
using System.Collections.Generic;
using System.Text;
using ToDoListNikeshina.Validators;

namespace ToDoListNikeshina
{
    public abstract class AbstractValidator : IValidate
    {
        protected IValidate _nextValidator;
        protected int _taskNumber;
        protected string _description;
        protected int _actionsCount;

        public int GetTaskNumber() => _taskNumber;

        public string GetDescription() => _description;

        public int GetActionsCount() => _actionsCount;

        public IValidate SetNext(IValidate validator)
        {
            _nextValidator = validator;
            return _nextValidator;
        }

        public abstract bool Validate();
    }
}
