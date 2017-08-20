using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    public class TestConditionStepUsingSecondBranch : IStep
    {
        public void Execute(Processor sender, int stepId)
        {
            sender.NextStepId += 3;
            sender.StepStatus[stepId] = Status.Successful;
        }
    }
}
