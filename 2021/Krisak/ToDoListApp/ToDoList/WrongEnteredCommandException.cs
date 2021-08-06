using System;

namespace ToDoLibrary
{
    public class WrongEnteredCommandException : Exception
    {
        public WrongEnteredCommandException(string message) : base(message) { }
    }
}
