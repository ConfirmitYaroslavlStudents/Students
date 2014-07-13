using System;

namespace ListLibrary
{
    internal class List<T>
    {
        private T[] _elements;
        private int _count;

        public List()
        {
            _elements = new T[5];
        }

        public List(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                _elements = new T[size];
            }
        }

        public int Capacity
        {
            get { return _elements.Length; }

            set
            {
                if (value < _count)
                {
                    throw new ArgumentException();
                }
                if (_elements.Length < value)
                {
                    if (value > 0)
                    {
                        T[] tempArray = new T[value];
                        Array.Copy(_elements, 0, tempArray, 0, _count);
                        _elements = tempArray;
                    }
                }
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public void Add(T element)
        {
            if (_count == Capacity)
            {
                Capacity = Capacity * 2;
            }
            _elements[_count] = element;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (index >= _count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (index < _count)
            {
                Array.Copy(_elements, index + 1, _elements, index, _count - index);
            }
            _elements[_count] = default(T);
            _count--;
        }

        public Boolean Remove(T element)
        {
            int index = Array.IndexOf<T>(_elements, element, 0, _count);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
