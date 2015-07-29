using System.Collections.Generic;
using Matrix.Writers.Commands;

namespace Matrix.Writers
{
    public class BufferWriter : IWriter
    {
        readonly Queue<IWriteCommand> _commands = new Queue<IWriteCommand>();
        private readonly IWriter _writer;

        public BufferWriter(IWriter writer)
        {
            _writer = writer;
        }

        public void Write(string value)
        {
            _commands.Enqueue(new WriteValueCommand(value));
        }

        public void WriteLine()
        {
            _commands.Enqueue(new WriteLineCommand());
        }

        public void Dispose()
        {
            foreach (var writeCommand in _commands)
            {
                writeCommand.Write(_writer);                
            }
        }
    }
}