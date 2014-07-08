using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StackWithUseArray
{
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        protected T[] _array;
        public int Length { get; private set; }

        public Stack()
        {
            this.Length = 0;
            this._array = new T[1];
        }

        public void Push(T newElement)
        {
            if (Length == _array.Length)
            {
                T[] tempArray = new T[2 * _array.Length];
                Array.Copy(_array, 0, tempArray, 0, Length);
                _array = tempArray;
            }

            Length++;
            _array[Length-1] = newElement;
        }

        public T Peek()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }

            return _array[Length - 1];
        }

        public T Pop() 
        {
            if (Length == 0)
           
                throw new InvalidOperationException("Stack is Empty");

            Length--;
            T temp =_array[Length];
            _array[Length] = default(T);
            return temp;

        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorAdapter<T>(_array, Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }

       
    }

    
   
}
