namespace AutomatizationSystemLib
{
    public class ConsoleCommandStep : IStep
    {
        private ConsoleCommandOptions _options;

        public ConsoleCommandStep(ConsoleCommandOptions options)
        {
            _options = options;
        }

        public void Execute(Processor sender, int stepId)
        {
            if (_options.IsIndependent || stepId == 0 || (!_options.IsIndependent && sender.StepStatus[stepId - 1] == Status.Successful))
            {
                try
                {
                    System.Diagnostics.Process.Start("cmd.exe", "/C " + _options.CommandName);

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
