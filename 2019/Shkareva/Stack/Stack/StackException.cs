using System;

namespace Stack
{
    public class StackException: Exception
    {
        public StackException(string message): base(message) { }
    }
}
