using AutoProcessor;

namespace AutoProcessorTests
{
    public class EmptyStep : Step
    {
        public EmptyStep()
        {
            StepStatus = Status.NotStarted;
        }

        public override void Start()
        {
            StepStatus = Status.Finished;
        }
    }
}
