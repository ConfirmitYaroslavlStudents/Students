using System.Text;

namespace CellsAutomate.Mutator.Mutations.Logging
{
    public class Logger : ILogger
    {
        public StringBuilder Builder { get; }

        public Logger()
        {
            Builder = new StringBuilder();
        }

        public void Write(string log)
        {
            Builder.AppendLine(log);
        }
    }
}