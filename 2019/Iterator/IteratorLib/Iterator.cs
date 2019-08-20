using System;
using System.Collections.Generic;

namespace IteratorLib
{
    // Notes
    // Map & Filter implemented in Iterator, not in IIterator
    // Potential solution: add M&F to Interfase - is not cool, because a lot of production code may use interface IIterator
    // Current implementation is not "Lazy" 

    public interface IIterator<T>
    {
        bool MoveNext();
        T Current { get; }
    }

    public class Iterator<T> : IIterator<T>
    {
        private T[] _array;
        private int _currentIndex;

        public Iterator(T[] array)
        {
            _array = array;
            _currentIndex = -1;
        }

        public T Current => _array[_currentIndex];

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _array.Length;
        }

        public Iterator<R> Map<R>(Func<T, R> func)
        {
            List<R> newIterator = new List<R>();

            for (int i = _currentIndex + 1; i < _array.Length; i++)
                newIterator.Add(func(_array[i]));

            return new Iterator<R>(newIterator.ToArray());

        }

        public Iterator<T> Filter(Func<T, bool> func)
        {

            List<T> output = new List<T>();

            for (int i = _currentIndex + 1; i < _array.Length; i++)
            {
                if(func(_array[i]))
                {
                    output.Add(_array[i]);
                }
            }

            return new Iterator<T>(output.ToArray());
        }
    }
}
