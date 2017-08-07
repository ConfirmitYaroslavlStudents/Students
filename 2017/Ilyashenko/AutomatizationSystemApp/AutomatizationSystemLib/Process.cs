using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatizationSystemLib
{
    public class Process : IProcessable
    {
        private List<IStep> _steps;
        private bool _stepsExecuteCorrectly = true;

        public Process(IEnumerable<IStep> steps)
        {
            _steps = steps.ToList();
        }

        public void Execute()
        {
            foreach (var step in _steps)
            {
                try
                {
                    step.Execute(_stepsExecuteCorrectly);
                }
                catch
                {
                    _stepsExecuteCorrectly = false;
                }
            }
        }

        public void AddStep(IStep step)
        {
            _steps.Add(step);
        }
    }
}
