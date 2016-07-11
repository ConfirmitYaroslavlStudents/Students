using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class MyQueue<T> : IEnumerable<T>
    {
        public int Count { private set; get; }
        Node<T> _head;
        Node<T> _lastNode;

        //Constructor
        public MyQueue()
        {
            Count = 0;
            _head = null;
            _lastNode = null;
        }

        //Adds an object to the end of the MyQueue<T>.
        public void Enqueue(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (Count == 0)
            {
                _head = newNode;
                _lastNode = newNode;
            }
            else
            {
                _lastNode.NextNode = newNode;
                _lastNode = newNode;
            }
            Count++;
        }

        //Removes and returns the object at the beginning of the MyQueue<T>.
        public T Dequeue()
        {
            if (Count != 0)
            {
                T returnedValue = _head.Value;
                _head = _head.NextNode;
                Count--;
                if (Count == 0)
                {
                    _lastNode = null;
                }
                return returnedValue;
            }
            else
            {
                throw new Exception("The queue is empty.");
            }
        }


        //Removes all objects from the MyQueue<T>.
        public void Clear()
        {
            _head = null;
            _lastNode = null;
            Count = 0;
        }

        //Returns the object at the beginning of the MyQueue<T> without removing it.
        public T Peek()
        {
            if (Count != 0)
            {
                return _head.Value;
            }
            else
            {
                throw new Exception("The queue is empty.");
            }
        }

        //Determines whether an element is in the MyQueue<T>.
        public bool Contains(T value)
        {
            Node<T> currentNode = _head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                    return true;
                currentNode = currentNode.NextNode;
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (Node<T> currentNode = _head; currentNode != null; currentNode = currentNode.NextNode)
            {
                yield return currentNode.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (Node<T> currentNode = _head; currentNode != null; currentNode = currentNode.NextNode)
            {
                yield return currentNode.Value;
            }
        }
    }
}
