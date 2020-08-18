using System;

namespace BillSplitter.Attributes
{
    public interface IValidator<T>
    {
        public IValidator<T> AddValidation(Func<T, bool> predicate);
        public bool Validate(T x);
    }
}