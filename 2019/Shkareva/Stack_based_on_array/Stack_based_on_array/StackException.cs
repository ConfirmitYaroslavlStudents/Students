using System;

namespace Stack_based_on_array
{
    public class StackException: Exception
    {
        public StackException(string message): base(message) { }
    }
}
