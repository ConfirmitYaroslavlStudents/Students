using System;
using System.Diagnostics;

namespace Tasker.Core.Actions.BatExecutor
{
    public class BatExecutorAction : IAction
    {
        private BatOptions Options { get; }

        public BatExecutorAction(BatOptions options)
        {
            Options = options ?? throw new ArgumentException(nameof(options));
        }

        public Status Execute()
        {
            ProcessStartInfo procStartInfo =
                new ProcessStartInfo(this.Options.Path, this.Options.Arguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

            Process proccess = new Process { StartInfo = procStartInfo };
            proccess.Start();

            return Status.Success;
        }
    }
}