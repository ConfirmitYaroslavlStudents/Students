using System;
using System.Collections.Generic;

namespace IteratorLib
{
    // Notes
    // Done: Map & Filter implemented in ArrayIterator, not in IIterator
    // Done: Potential solution: add M&F to Interfase - is not cool, because a lot of production code may use interface IIterator
    // Done: Current implementation is not "Lazy" 

    public interface IIterator<T>
    {
        bool MoveNext();
        T Current { get; }
    }

    public class ArrayIterator<T> : IIterator<T>
    {
        private T[] _array;
        private int _currentIndex;

        public ArrayIterator(T[] array)
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
    }
}
