using AutoProcessor;

namespace AutoProcessorTests
{
    public class StatusStep : Step
    { 
        public StatusStep(Status myStatus)
        {
            StepStatus = myStatus;
        }

        public override void Start()
        { }
    }
}
