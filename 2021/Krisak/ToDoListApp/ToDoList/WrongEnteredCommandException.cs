using System;
using System.Collections.Generic;

namespace ToDoLibrary
{
    public class WrongEnteredCommandException : Exception
    {
        public WrongEnteredCommandException(string message) : base(message) { }
    }
}
