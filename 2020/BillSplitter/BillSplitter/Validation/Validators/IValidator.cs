using System;

namespace BillSplitter.Validation.Validators
{
    public interface IValidator<T>
    {
        public IValidator<T> AddValidation(Func<T, bool> predicate);
        public bool Validate(T x);
    }
}