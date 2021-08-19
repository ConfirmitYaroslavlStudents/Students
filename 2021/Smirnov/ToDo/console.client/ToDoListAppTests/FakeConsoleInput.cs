using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Reader;

namespace ToDoListAppTests
{
    class FakeConsoleInput : IConsoleInput
    {
        public string Input { set; get; }
        public string ReadLine()
        {
            return Input;
        }
    }
}
