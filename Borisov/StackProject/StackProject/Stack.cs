using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackProject
{
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
                throw new InvalidOperationException("MyStack is Emty");
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
                throw new InvalidOperationException("MyStack is Emty");

            }
            else
            {
                T temp = _peek.Value;
                _peek = _peek.Previous;
                Length--;
                return temp;
            }
        }
        private T ToArray_index(int index)
        {
            T[] temp = new T[index + 1];

            for (int i = 0; i <= index; i++)
            {
                temp[i] = Pop();
            }
            T element = temp[index];
            for (int i = index; i >= 0; i--)
            {
                Push(temp[i]);
            }
            return element;

        }
        public Stack<T>.Enumerator GetEnumerator()
        {
            return new Stack<T>.Enumerator(this);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)new Stack<T>.Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new Stack<T>.Enumerator(this);
        }
        public class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private Stack<T> _stack;
            private T currentElement;
            private int _index = -1;
            public Enumerator(Stack<T> stack)
            {
                this._stack = stack;
                this._index = -1;
                this.currentElement = default(T);
            }
            public T Current
            {
                get
                {
                    if (_index == -1)
                    {
                        throw new InvalidOperationException("MyStack is Emty");
                    }
                    else
                    {
                        return this.currentElement;
                    }
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    if (_index == -1)
                    {
                        throw new InvalidOperationException("MyStack is Emty");
                    }
                    else
                    {
                        return (object)this.currentElement;
                    }

                }
            }
            public void Dispose()
            {
                this._index = _stack.Length;
            }
            void IEnumerator.Reset()
            {
                this._index = -1;
                this.currentElement = default(T);
            }
            public bool MoveNext()
            {
                if (this._index == -1)
                {
                    this._index = 0;
                    bool can_move = this._index >= 0;
                    if (can_move)
                    {
                        this.currentElement = this._stack.ToArray_index(_index);
                    }
                    return can_move;
                }
                else
                {
                    if (this._index == _stack.Length)
                    {
                        return false;
                    }
                    bool can_move = ++this._index <= _stack.Length - 1;
                    if (can_move)
                    {
                        this.currentElement = this._stack.ToArray_index(_index);
                    }
                    return can_move;
                }

            }

        }

    }
}
