using System.Collections.Generic;
using System.Linq;

namespace AutomatizationSystemLib
{
    public class Processor
    {
        private List<IStep> _steps;
        public Status[] StepStatus;
        public int NextStepId = 0;

        public Processor(IEnumerable<IStep> steps)
        {
            _steps = steps.ToList();
        }

        public void Execute()
        {
            InitializeStatus();

            while (NextStepId < _steps.Count)
            {
                _steps[NextStepId].Execute(this, NextStepId);
            }
        }

        public void AddStep(IStep step)
        {
            _steps.Add(step);
        }

        private void InitializeStatus()
        {
            StepStatus = new Status[_steps.Count];
            for (int i = 0; i < StepStatus.Length; i++)
                StepStatus[i] = Status.Waiting;
        }
    }
}
