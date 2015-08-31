using System.Text;
using CommandCreation;

namespace Tests.Fakes
{
    public class FakeWriter : IWorker
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

        public string ReadLine()
        {
            throw new System.NotImplementedException();
        }
    }
}
