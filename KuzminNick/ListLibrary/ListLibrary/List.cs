using System;
using System.Collections.Generic;
using System.Collections;

namespace ListLibrary
{
    public class List<T> : IList<T>
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

            _elements = new T[size];
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
                if (_elements.Length >= value) return;
                if (value <= 0) return;

                var tempArray = new T[value];
                Array.Copy(_elements, 0, tempArray, 0, _count);
                _elements = tempArray;
            }
        }

        public int Count
        {
            get { return _count; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                _count = value;
            }
        }

        public void Add(T element)
        {
            if (Count == Capacity)
            {
                Capacity = Capacity * 2;
            }
            _elements[Count] = element;
            Count++;
        }

        public Boolean Remove(T element)
        {
            var index = Array.IndexOf(_elements, element, 0, _count);
            if (index < 0) return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Count--;
            Array.Copy(_elements, index + 1, _elements, index, Count - index);
            _elements[Count] = default(T);
        }        

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return _elements[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
                if (Equals(_elements[i], item))
                    return i;

            return -1;
        }

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
                if (index < Count && index >= 0)
                    return _elements[index];

                throw new IndexOutOfRangeException();
            }

            set
            {
                if (index < Count && index >= 0)
                    _elements[index] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public void Clear()
        {
            if (Count <= 0) return;

            _elements = new T[5];
            Count = 0;
            Capacity = 5;
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
                if (Equals(_elements[i], item))
                    return true;

            return false;
        }        
        
        public void CopyTo(T[] destinationArray, int indexDestinationArray)
        {
            if (destinationArray == null)
                throw new ArgumentNullException();
            if ((destinationArray.Length - indexDestinationArray) < Count)
                throw new ArgumentOutOfRangeException();
            
            Array.Copy(_elements, 0, destinationArray, indexDestinationArray, Count);
        }

        private bool IsPlacedInNewArray(T[] destinationArray, int indexDestinationArray)
        {
            return Count > (destinationArray.Length - indexDestinationArray);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public string Print()
        {
            var allElementsInStringFormat = String.Empty;
            for (var i = 0; i < Count; i++)
                allElementsInStringFormat += _elements[i].ToString() + Environment.NewLine;

            return allElementsInStringFormat;
        }
    }
}
