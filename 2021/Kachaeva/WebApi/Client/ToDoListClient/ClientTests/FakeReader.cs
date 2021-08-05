using System.Collections.Generic;
using ToDoClient;

namespace ClientTests
{
    public class FakeReader : IReader
    {
        private readonly List<string> _input;
        private int _inputIndex;

        public FakeReader(List<string> inputs)
        {
            _input = inputs;
            _inputIndex = -1;
        }
        public string ReadLine()
        {
            _inputIndex++;
            return _input[_inputIndex];
        }
    }
}
