using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public class TestWriterReader : IWriterReader
    {
        public List<string> _messages = new List<string>();
        public List<string> _inputs;
        private int _inputIndex;

        public TestWriterReader(List<string> inputs)
        {
            _inputs = inputs;
            _inputIndex = -1;
        }

        public void Write(string message)
        {
            _messages.Add(message);
        }

        public string Read()
        {
            _inputIndex++;
            return _inputs[_inputIndex];
        }
    }
}
