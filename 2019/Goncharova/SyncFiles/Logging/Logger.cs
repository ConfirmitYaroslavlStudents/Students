using System.Collections.Generic;

namespace Logging
{
    public class Logger : ILogger
    {
        public Configuration Configuration { get; internal set; }

        public void Summary(string message)
        {
            var targets = Configuration.GetTargetsWithLevel(LogLevel.Summary);
            if (targets != null)
            {
                WriteToTargets(targets, message);
            }
        }

        public void Verbose(string message)
        {
            var targets = Configuration.GetTargetsWithLevel(LogLevel.Verbose);
            if (targets != null)
            {
                WriteToTargets(targets, message);
            }
        }

        private void WriteToTargets(List<ITarget> targets, string message)
        {
            foreach (var target in targets)
            {
                target.Write(message);
            }
        }
    }
}
