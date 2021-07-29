using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public class WrongEnteredCommandException: Exception
    {
        public WrongEnteredCommandException(string message): base(message) { }
    }
}
