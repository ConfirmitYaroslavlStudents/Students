using System;
using System.Collections.Generic;

namespace Matrix.Writers
{
    public class CompositeWriter : IWriter
    {
        private readonly IEnumerable<IWriter> _writers;

        public CompositeWriter(IEnumerable<IWriter> writers)
        {
            _writers = writers;
        }

        public void Write(string value)
        {
            DoForEachWriter(x => x.Write(value));
        }

        public void WriteLine()
        {
            DoForEachWriter(x => x.WriteLine());
        }

        public void Dispose()
        {
            DoForEachWriter(x => x.Dispose());
        }

        private void  DoForEachWriter(Action<IWriter> action)
        {
            foreach (var writer in _writers)
            {
                action.Invoke(writer);
            }
        }
    }
}