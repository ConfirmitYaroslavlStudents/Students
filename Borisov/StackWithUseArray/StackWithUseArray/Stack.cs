using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StackWithUseArray
{
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        private T[] _array;
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

        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new StackEnumerator(this);
        }

        public class StackEnumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private T _currentElement;
            private int _index = -1;
            private Stack<T> _stack;

            public StackEnumerator(Stack<T> stack)
            {
                this._stack = stack;
                this._index = -1;
                this._currentElement = default(T);
            }

            public T Current
            {
                get
                {
                    if (_index == _stack.Length)

                        throw new InvalidOperationException("MyStack is Empty");

                    else

                        return this._currentElement;

                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == _stack.Length)

                        throw new InvalidOperationException("MyStack is Empty");

                    else

                        return (object)this._currentElement;


                }
            }

            public void Dispose()
            {
                this._index = _stack.Length;
            }

            void IEnumerator.Reset()
            {
                this._index = -1;
                this._currentElement = default(T);
            }

            public bool MoveNext()
            {
                ++this._index;
                bool canMove = this._index >= 0 && this._index <= _stack.Length - 1;

                if (canMove)
                {
                    _currentElement = _stack._array[_stack.Length-_index-1];
                }

                return canMove;

            }

        }
    }

    
   
}
