using System.Collections.Generic;

namespace ToDo
{
    public class TestWriterReader : IWriterReader
    {
        private readonly List<string> _inputs;
        private int _inputIndex;
        public List<string> Messages { get; private set; }

        public TestWriterReader(List<string> inputs)
        {
            _inputs = inputs;
            _inputIndex = -1;
            Messages = new List<string>();
        }

        public TestWriterReader() : this(new List<string>()) { }

        public void Write(string message)
        {
            Messages.Add(message);
        }

        public string Read()
        {
            _inputIndex++;
            return _inputs[_inputIndex];
        }
    }
}