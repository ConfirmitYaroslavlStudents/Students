using System;

namespace AutomatizationSystemLib
{
    public class ConsoleWriteStep : IStep
    {
        private ConsoleWriteOptions _options;

        public ConsoleWriteStep(ConsoleWriteOptions options)
        {
            _options = options;
        }

        public void Execute(Processor sender, int stepId)
        {
            try
            {
                Console.WriteLine(_options.Message);
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
