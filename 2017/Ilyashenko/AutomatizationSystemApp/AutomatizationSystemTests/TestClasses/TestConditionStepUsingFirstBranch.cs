using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    public class TestConditionStepUsingFirstBranch : IStep
    {
        public void Execute(Processor sender, int stepId)
        {
            sender.NextStepId++;
            sender.StepStatus[stepId] = Status.Successful;
        }
    }
}
