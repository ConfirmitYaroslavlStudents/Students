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
            : this(5) //I think we should use 4 because 5 is not power of 2
        { }

        public List(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _elements = new T[size];
            Count = 0;
        }

        public int Capacity
        {
            get { return _elements.Length; }

            private set
            {
                if (value < Count || value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                var extendableArray = new T[value];
                Array.Copy(_elements, 0, extendableArray, 0, Count);
                _elements = extendableArray;
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
            var indexOfNextElement = index + 1;
            Array.Copy(_elements, indexOfNextElement, _elements, destinationIndex: index, length: Count - index);
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
            if (indexInsertedItem >= Count)
                throw new ArgumentOutOfRangeException();

            if (Count == Capacity)
                Capacity = Capacity * 2;

            var indexOfNextElement = indexInsertedItem + 1;
            var lengthRestOfArray = Count - indexInsertedItem;

            Array.Copy(_elements, indexInsertedItem, _elements, indexOfNextElement, lengthRestOfArray);
        }

        public T this[int index]
        {
            get
            {
                if (IsCorrectIndex(index))
                    return _elements[index];

                throw new IndexOutOfRangeException();
            }

            set
            {
                if (IsCorrectIndex(index))
                    _elements[index] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        private bool IsCorrectIndex(int index)
        {
            return index < Count && index >= 0;
        }

        public void Clear()
        {
            if (Count <= 0) return;

            _elements = new T[5];
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
                if (Equals(_elements[i], item))
                    return true;

            return false;
        }        
        
        public void CopyTo(T[] destinationArray, int indexForInsertingDestinationArray)
        {
            if (destinationArray == null)
                throw new ArgumentNullException();
            if ( !(IsPlacedInNewArray(destinationArray, indexForInsertingDestinationArray)) )
                throw new ArgumentOutOfRangeException();
            
            Array.Copy(_elements, 0, destinationArray, indexForInsertingDestinationArray, Count);
        }

        //ReSharper suggests to use ICollection, due to our last lecture I think we should use it
        private bool IsPlacedInNewArray(T[] destinationArray, int indexDestinationArray)
        {
            return Count < (destinationArray.Length - indexDestinationArray);
        }

        //I don't know why do we need this property
        public bool IsReadOnly
        {
            get { return false; }
        }

        public string Print()
        {
            var allElementsInStringFormat = String.Empty;
            for (var i = 0; i < Count; i++)
                allElementsInStringFormat += _elements[i] + Environment.NewLine;

            return allElementsInStringFormat;
        }
    }
}
