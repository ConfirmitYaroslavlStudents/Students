using System.Collections.Generic;
using AutomatedTasker.StepConfig;

namespace AutomatedTasker
{
    public class ExecutingInfo
    {
        public List<IStepConfig> Steps { set; get; }
        public bool HasFailed { set; get; }
    }
}
