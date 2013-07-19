using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackProject
{
    public class StackEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private T[] _stackArray;
        private Stack<T> _stack;
        private T _currentElement;
        private int _index = -1;

        public StackEnumerator(Stack<T> stack)
        {
            _stackArray = new T[stack.Length];

            int length = stack.Length;
            for (int i = 0; i < length; i++)
            {
                _stackArray[i] = stack.Pop();
            }

            for (int i = length - 1; i >= 0; i--)
            {
                stack.Push(_stackArray[i]);
            }

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
                this._currentElement = this._stackArray[_index];
            }

            return canMove;

        }

    }
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        private class StackElement
        {
            public StackElement Previous { get; private set; }
            public T Value { get; set; }

            public StackElement(T value, StackElement previous)
            {
                this.Value = value;
                this.Previous = previous;
            }
        }
        public int Length { get; private set; }
        private StackElement _peek;

        public Stack()
        {
            this.Length = 0;
            this._peek = null;
        }

        public void Push(T newElement)
        {
            StackElement temp = new StackElement(newElement, _peek);
            _peek = temp;
            Length++;
        }

        public T Peek()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException("MyStack is Empty");
            }
            else
            {
                return _peek.Value;
            }
        }

        public T Pop()
        {
            if (Length <= 0)
            {
                throw new InvalidOperationException("MyStack is Empty");

            }
            else
            {
                T temp = _peek.Value;
                _peek = _peek.Previous;
                Length--;
                return temp;
            }
        }

        public StackEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator<T>(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)new StackEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new StackEnumerator<T>(this);
        }

    }
}
