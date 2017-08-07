using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class Process
    {
        private List<StepDependensyDecorator> _steps;
        
        public Process()
        {
            _steps = new List<StepDependensyDecorator>();
        }

        public Process(IEnumerable<StepDependensyDecorator> steps)
        {
            _steps = new List<StepDependensyDecorator>(steps);
        }

        public void AddStep(StepDependensyDecorator newStep)
        {
            _steps.Add(newStep);
        }

        public List<StepDependensyDecorator> Steps
        {
            get
            {
                return _steps;
            }
        }
    }
}
