using System;
using System.Collections.Generic;
using System.Text;
using ToDo;

namespace ToDoListTests
{
    public class TestReader : IReader
    {
        private readonly List<string> _inputs;
        private int _inputIndex;

        public TestReader(List<string> inputs)
        {
            _inputs = inputs;
            _inputIndex = -1;
        }
        public string ReadLine()
        {
            _inputIndex++;
            return _inputs[_inputIndex];
        }
    }
}
