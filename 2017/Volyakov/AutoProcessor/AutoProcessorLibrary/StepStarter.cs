using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class StepStarter
    {
        private bool _flag;

        public StepStarter()
        {
            _flag = true;
        }

        public bool StartStep(StepDependensyDecorator currentStep)
        {
            try
            {
                _flag = currentStep.Start(_flag);
            }
            catch
            {
                _flag = false;
            }

            return _flag;
        }
    }
}
