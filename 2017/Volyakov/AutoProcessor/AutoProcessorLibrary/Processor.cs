using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class Processor
    {
        private StepStarter _starter;

        public Processor()
        {
            _starter = new StepStarter();
        }

        public void StartProcess(Process currentProcess)
        {
            var steps = currentProcess.Steps;

            foreach (var currentStep in steps)
            {
                _starter.StartStep(currentStep);
            }
        }
    }
}
