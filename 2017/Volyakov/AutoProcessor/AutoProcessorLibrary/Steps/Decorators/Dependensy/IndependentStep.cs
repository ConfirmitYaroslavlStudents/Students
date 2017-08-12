using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class IndependentStep: StepDependensyDecorator
    {
        private Step _step;

        public IndependentStep(Step step)
        {
            _step = step;
        }

        public override bool Start(bool flag)
        {
            return _step.Start();
        }
    }
}
