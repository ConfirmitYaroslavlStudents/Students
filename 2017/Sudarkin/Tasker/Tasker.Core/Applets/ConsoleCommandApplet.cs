using System;
using System.Diagnostics;
using Tasker.Core.Options;

namespace Tasker.Core.Applets
{
    public class ConsoleCommandApplet : IApplet<ConsoleOptions>
    {
        public ExecutionCondition Condition { get; }
        public ConsoleOptions Options { get; }

        public ConsoleCommandApplet(ConsoleOptions options)
            : this(ExecutionCondition.Always, options)
        {

        }

        public ConsoleCommandApplet(ExecutionCondition condition, ConsoleOptions options)
        {
            Condition = condition;
            Options = options ?? throw new ArgumentException(nameof(condition));
        }

        public State Execute()
        {
            try
            {
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo("cmd", "/c " + Options.Command)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                Process proccess = new Process {StartInfo = procStartInfo};
                proccess.Start();

                return State.Successful;
            }
            catch
            {
                return State.Failed;
            }
        }
    }
}