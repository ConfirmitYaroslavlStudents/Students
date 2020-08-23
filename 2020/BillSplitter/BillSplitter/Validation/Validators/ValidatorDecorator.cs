using System;

namespace BillSplitter.Validation.Validators
{
    public class ValidatorDecorator<T> : IValidator<T>
    {
        private Func<T, bool> _predicate = x => true;
        private IValidator<T> _decorated;

        public ValidatorDecorator(IValidator<T> decorated)
        {
            if (decorated == null)
                throw new ArgumentNullException("Decorated decorated must not be null");

            _decorated = decorated;
        }

        public IValidator<T> AddValidation(Func<T, bool> predicate)
        {
            _predicate = predicate;
            return new ValidatorDecorator<T>(this);
        }

        public bool Validate(T x)
        {
            return _predicate(x) && _decorated.Validate(x);
        }
    }
}