using System;

namespace StackLinkedList
{
    public class StackException: Exception
    {
        public StackException(string message): base(message) { }
    }
}
