using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public class TestWriterReader : IWriterReader
    {
        public List<string> _messages = new List<string>();
        public string _input;

        public void Write(string message)
        {
            _messages.Add(message);
        }

        public string Read()
        {
            return _input;
        }
    }
}
