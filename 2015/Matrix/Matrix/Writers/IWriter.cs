using System;

namespace Matrix.Writers
{
    public interface IWriter : IDisposable
    {
        void Write(string value);
        void WriteLine();
    }
}