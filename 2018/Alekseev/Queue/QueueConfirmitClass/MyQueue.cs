using System.Security.Policy;

namespace QueueConfirmitClass
{
    public abstract class MyQueue<T>
    {
        public abstract void Dequeue();

        public abstract void Enqueue(T element);

        public abstract int Count();

        public abstract T Peek();

        public abstract void Clear();

        public abstract bool Contains(T element);

        public virtual void Print()
        {          
        }

    }
}