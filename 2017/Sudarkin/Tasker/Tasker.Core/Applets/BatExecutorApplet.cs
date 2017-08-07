using System;
using System.Diagnostics;
using Tasker.Core.Options;

namespace Tasker.Core.Applets
{
    public class BatExecutorApplet : IApplet<BatOptions>
    {
        public ExecutionCondition Condition { get; }
        public BatOptions Options { get; }

        public BatExecutorApplet(BatOptions options)
            : this(ExecutionCondition.Always, options)
        {

        }

        public BatExecutorApplet(ExecutionCondition condition, BatOptions options)
        {
            Condition = condition;
            Options = options ?? throw new ArgumentException(nameof(condition));
        }

        public State Execute()
        {
            try
            {
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo(Options.Path, Options.Arguments)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                Process proccess = new Process { StartInfo = procStartInfo };
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