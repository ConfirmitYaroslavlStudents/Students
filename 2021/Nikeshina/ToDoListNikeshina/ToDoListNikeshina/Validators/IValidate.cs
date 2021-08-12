using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina.Validators
{
    public interface IValidate
    {
        public IValidate SetNext(IValidate validator);
        public bool Validate();
    }
}
