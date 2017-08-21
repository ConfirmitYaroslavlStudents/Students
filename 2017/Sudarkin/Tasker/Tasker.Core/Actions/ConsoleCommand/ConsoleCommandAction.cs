using System;
using System.Diagnostics;

namespace Tasker.Core.Actions.ConsoleCommand
{
    public class ConsoleCommandAction : IAction
    {
        private ConsoleOptions Options { get; }

        public ConsoleCommandAction(ConsoleOptions options)
        {
            Options = options ?? throw new ArgumentException(nameof(options));
        }

        public Status Execute()
        {
            ProcessStartInfo procStartInfo =
                new ProcessStartInfo("cmd", "/c " + this.Options.Command)
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