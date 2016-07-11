using System;
using System.Text;

namespace QueueArray
{
	public class Queue<T>
	{
		private T[] _elements;
		private int _head;
		private int _tail;

		public int Count { get; private set; }
		public int Capacity { get; private set; }

		public Queue() : this(5)
		{
		}

		public Queue(int capacity)
		{
			_elements = new T[capacity];
			_head = 0;
			_tail = 0;
			Count = 0;
			Capacity = capacity;
		}

		public T Pop()
		{
			if (IsEmpty())
			{
				throw new Exception("Error! Queue is empty!");
			}
			int lastHeadPosition = _head;
			Move(ref _head);
			Count--;
			return _elements[lastHeadPosition];
		}

		public void Push(T element)
		{
			if (IsFull())
			{
				DoubleQueue();
			}
			_elements[_tail] = element;
			Move(ref _tail);
			Count++;
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}

		public bool IsFull()
		{
			return Count == _elements.Length;
		}

		public T[] GetElements()
		{
			T[] result = new T[Count];
			int curr = _head;
			for (int i = 0; i < Count; i++)
			{
				result[i] = _elements[curr];
				Move(ref curr);
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			int curr = _head;
			for (int i = 0; i < Count; i++) 
			{
				result.Append(_elements[curr] + " ");
				Move(ref curr);
			}
			return result.ToString();
		}

		private void Move(ref int queuePointer)
		{
			queuePointer = (queuePointer + 1) % _elements.Length;
		}

		private void DoubleQueue()
		{
			T[] doubledQueue = new T[_elements.Length*2];
			if (_head < _tail)
			{
				Array.Copy(_elements, _head, doubledQueue, 0, Count);
			}
			else
			{
				Array.Copy(_elements, _head, doubledQueue, 0, _elements.Length - _head);
				Array.Copy(_elements, 0, doubledQueue, _elements.Length - _head, _tail);
			}
			_elements = doubledQueue;
			_head = 0;
			_tail = Count;
			Capacity = doubledQueue.Length;
		}
	}
}
