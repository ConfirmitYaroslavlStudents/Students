using System;

namespace AutomatizationSystemLib
{
    public class RandomConditionStep : IStep
    {
        public void Execute(Processor sender, int stepId)
        {
            var rnd = new Random();
            if (rnd.Next(0, 2) % 2 == 0)
            {
                sender.NextStepId++;
            }
            else
            {
                sender.NextStepId += 3;
            }
            sender.StepStatus[stepId] = Status.Successful;
        }
    }
}
