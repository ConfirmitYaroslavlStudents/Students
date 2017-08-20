using System.Collections.Generic;

namespace AutoProcessor
{
    public class Process
    {
        private List<Step> _steps;

        public Process()
        {
            _steps = new List<Step>();
        }

        public Process(IEnumerable<Step> steps)
        {
            _steps = new List<Step>(steps);
        }

        public void AddStep(Step newStep)
        {
            _steps.Add(newStep);
        }

        public List<Step> Steps
        {
            get
            {
                return _steps;
            }
        }
    }
}
