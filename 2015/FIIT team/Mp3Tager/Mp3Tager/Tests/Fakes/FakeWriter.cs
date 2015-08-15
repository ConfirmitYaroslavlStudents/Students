using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCreation;

namespace Tests.Fakes
{
    public class FakeWriter : IWriter
    {
        public StringBuilder Stream { get; private set; }

        public FakeWriter()
        {
            Stream = new StringBuilder();
        }

        public void Write(string value)
        {
            Stream.Append(value);
        }

        public void WriteLine()
        {
            Stream.Append("\n");
        }

        public void WriteLine(string value)
        {
            Stream.Append(value + "\n");
        }
    }
}
