using System;

namespace IteratorLib
{
    internal class FilterDecorator<T> : IIterator<T>
    {
        public FilterDecorator(IIterator<T> iterator, Predicate<T> condition)
        {
            this.iterator = iterator;
            this.condition = condition;
        }

        private readonly IIterator<T> iterator;
        private readonly Predicate<T> condition;

        public T Current => iterator.Current;

        public bool MoveNext()
        {
            if(iterator.MoveNext())
            {
                if (condition(iterator.Current))
                    return true;

                MoveNext();
            }

            return false;
        }
    }
}