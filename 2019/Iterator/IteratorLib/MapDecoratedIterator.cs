using System;

namespace IteratorLib
{
    public class MapDecoratedIterator<T, R> : IIterator<R>
    {
        private IIterator<T> _wrapped;
        private Func<T, R> _map;

        public MapDecoratedIterator (IIterator<T> iterator, Func<T, R> map)
        {
            _wrapped = iterator;
            _map = map;
        }

        public bool MoveNext()
        {
            return _wrapped.MoveNext();
        }

        public R Current => _map(_wrapped.Current);
    }
}