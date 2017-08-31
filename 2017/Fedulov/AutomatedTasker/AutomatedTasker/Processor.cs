 using System.Collections.Generic;
 using AutomatedTasker.StepConfig;

namespace AutomatedTasker
{
    public class Processor
    {
        public ExecutingInfo Info;

        public Processor(List<IStepConfig> steps)
        {
            Info = new ExecutingInfo
            {
                Steps = steps,
                HasFailed = false
            };
        }

        private bool CheckCondition(IStepConfig step)
            => Info.HasFailed && step.Condition.Always || !Info.HasFailed;

        public void Process()
        {
            for(int i = 0; i < Info.Steps.Count; ++i)
            {
                var step = Info.Steps[i];
                if (CheckCondition(step))
                    step.Execute(Info, i);
            }
        }
    }
}
