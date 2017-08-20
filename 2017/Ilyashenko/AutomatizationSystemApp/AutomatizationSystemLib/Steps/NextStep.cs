namespace AutomatizationSystemLib
{
    public class NextStep : IStep
    {
        private NextStepOptions _options;

        public NextStep(NextStepOptions options)
        {
            _options = options;
        }

        public void Execute(Processor sender, int stepId)
        {
            sender.NextStepId = _options.NextStep;
            sender.StepStatus[stepId] = Status.Successful;
        }
    }
}
