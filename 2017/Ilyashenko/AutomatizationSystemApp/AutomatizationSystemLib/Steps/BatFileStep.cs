using System.Diagnostics;

namespace AutomatizationSystemLib
{
    public class BatFileStep : IStep
    {
        private BatFileOptions _options;

        public BatFileStep(BatFileOptions options)
        {
            _options = options;
        }

        public void Execute(Processor sender, int stepId)
        {
            if (_options.IsIndependent || stepId == 0 || (!_options.IsIndependent && sender.StepStatus[stepId-1] == Status.Successful))
            {
                try
                {
                    var processInfo = new ProcessStartInfo(_options.FileName, _options.Arguments)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    var process = new Process { StartInfo = processInfo };
                    process.Start();

                    sender.StepStatus[stepId] = Status.Successful;
                }
                catch
                {
                    sender.StepStatus[stepId] = Status.Failed;
                }
                sender.NextStepId++;
            }
        }
    }
}
