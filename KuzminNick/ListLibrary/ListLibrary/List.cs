using System;
using System.Collections.Generic;
using System.Collections;

namespace ListLibrary
{
    internal class List<T> : IEnumerable<T>, IList<T>
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

            private set
            {
                if (value < Count)
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
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                else
                    _count = value;
            }
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

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _elements[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (Equals(_elements[i], item))
                    return i;

            return -1;
        }

        // Отрефакторить +1
        public void Insert(int indexInsertedItem, T item)
        {
            if (Count < indexInsertedItem)
                throw new ArgumentOutOfRangeException();

            if (Count == Capacity)
                Capacity = Capacity * 2;

            Array.Copy(_elements, indexInsertedItem, _elements, indexInsertedItem + 1, Count - indexInsertedItem);
        }

        public T this[int index]
        {
            get
            {
                if (index < Count)
                    return _elements[index];
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (index < Count)
                    _elements[index] = value;
                else
                    throw new NotImplementedException();
            }
        }

        public void Clear()
        {
            if (Count > 0)
            {
                _elements = new T[5];
                Count = 0;
                Capacity = 5;
            }
        }

        // Протестить случай с null
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
                if (Equals(_elements[i], item))
                    return true;

            return false;

            //if (item == null)
            //{
            //    for(int i = 0; i < Count; i++)
            //        if (_elements[i] == null)
            //            return true;

            //    return false;
            //}
            //else
            //{
            //    for (int i = 0; i < Count; i++)
            //        if (Equals(_elements[i], item))
            //            return true;

            //    return false;
            //}
        }
        
        // Проверить, что если:
        // ьзовать массив размерности больше 2
        // поместить массив null
        // некорректный индекс
        public void CopyTo(T[] destinationArray, int indexDestinationArray)
        {
            Array.Copy(_elements, 0, destinationArray, indexDestinationArray, Count);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public string Print()
        {
            string allElementsInStringFormat = String.Empty;
            for (int i = 0; i < Count; i++)
                allElementsInStringFormat += _elements[i].ToString() + Environment.NewLine;

            return allElementsInStringFormat;
        }
    }
}
