using System;

namespace Tasker.Core.Actions.WriteLine
{
    public class WriteLineAction : IAction
    {
        private WriteLineOptions Options { get; }

        public WriteLineAction(WriteLineOptions options)
        {
            Options = options ?? throw new ArgumentException(nameof(options));
        }

        public Status Execute()
        {
            Console.WriteLine(Options.Message);
            return Status.Success;
        }
    }
}