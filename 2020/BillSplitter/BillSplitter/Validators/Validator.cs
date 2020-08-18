using System;

namespace BillSplitter.Attributes
{
    public class Validator<T> : IValidator<T>
    {
        private Func<T, bool> _predicate = x => true;

        public IValidator<T> AddValidation(Func<T, bool> predicate)
        {
            _predicate = predicate;
            return new ValidatorDecorator<T>(this);
        }

        public bool Validate(T x)
        {
            return _predicate(x);
        }
    }
}