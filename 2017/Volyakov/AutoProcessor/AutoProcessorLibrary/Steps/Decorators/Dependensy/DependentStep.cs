using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class DependentStep: StepDependensyDecorator
    {
        private Step _step;

        public DependentStep (Step step)
        {
            _step = step;
        }

        public override bool Start(bool flag)
        {
            if(flag)
                return _step.Start();

            return false;
        }
    }
}
