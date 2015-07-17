using System.Collections.Generic;

namespace QueueLibrary
{
    public interface IQueue<T>: IEnumerable<T>
    {
        /// <summary>
        /// Returns amount of items in the queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds item into a queue
        /// </summary>
        /// <param name="item">Item added to queue.</param>
        void Enqueue(T item);

        /// <summary>
        /// Returns an item with removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        T Dequeue();

        /// <summary>
        /// Returns an item without removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        T Peek();

        /// <summary>
        /// Delete all the items in a queue.
        /// </summary>
        void Clear();
    }
}
