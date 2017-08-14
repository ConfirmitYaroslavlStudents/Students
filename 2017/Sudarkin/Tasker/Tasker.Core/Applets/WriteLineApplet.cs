using System;
using Tasker.Core.Options;

namespace Tasker.Core.Applets
{
    public class WriteLineApplet : IApplet
    {
        public int Condition { get; }
        public WriteLineOptions Options { get; }

        public WriteLineApplet(WriteLineOptions options)
            : this(ExecutionCondition.Always, options)
        {

        }

        public WriteLineApplet(int condition, WriteLineOptions options)
        {
            Condition = condition;
            Options = options ?? throw new ArgumentException(nameof(condition));
        }

        public State Execute()
        {
            Console.WriteLine(Options.Message);
            return State.Successful;
        }
    }
}