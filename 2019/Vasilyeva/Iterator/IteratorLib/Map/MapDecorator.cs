using System;

namespace IteratorLib
{
    public class MapDecorator<T, R> : IIterator<R>
    {
        public MapDecorator(IIterator<T> iterator, Func<T, R> func)
        {
            this.iterator = iterator;
            this.func = func;
        }

        private readonly IIterator<T> iterator;
        private readonly Func<T, R> func;

        public R Current => func(iterator.Current);

        public bool MoveNext()
        {
            return iterator.MoveNext();
        }
    }
}