using System;

namespace IteratorLib
{
    public class FilterDecoratedIterator<T> : IIterator<T>
    {
        private IIterator<T> _wrapped;
        private Func<T, bool> _filter;

        public FilterDecoratedIterator(IIterator<T> iterator, Func<T, bool> filter)
        {
            _wrapped = iterator;
            _filter = filter;
        }

        public bool MoveNext()
        {
            bool canMove = false;
            while ((canMove = _wrapped.MoveNext()) && !_filter(_wrapped.Current))
                ;

            return canMove;
        }

        public T Current => _wrapped.Current;
    }
}