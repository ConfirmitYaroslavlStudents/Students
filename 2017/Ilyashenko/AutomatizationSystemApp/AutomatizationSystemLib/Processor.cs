using System.Collections.Generic;
using System.Linq;

namespace AutomatizationSystemLib
{
    public class Processor
    {
        public int NextStepId = 0;
        public ProcessorInfo Info;

        public Processor(IEnumerable<StepConfig> steps)
        {
            Info = new ProcessorInfo();
            Info.Steps = steps.ToList();
        }

        public void Execute()
        {
            InitializeStatus();

            while (NextStepId < Info.Steps.Count)
            {
                if (ExecutionConditionChecker.CheckCondition(Info.Steps[NextStepId].Condition, Info, NextStepId))
                    Info.Steps[NextStepId].Execute(Info, NextStepId);
                NextStepId++;
            }
        }

        public void AddStep(StepConfig step)
        {
            Info.Steps.Add(step);
        }

        private void InitializeStatus()
        {
            Info.StepStatus = new List<Status>();
            for (int i = 0; i < Info.Steps.Count; i++)
                Info.StepStatus.Add(Status.Waiting);
        }
    }
}
