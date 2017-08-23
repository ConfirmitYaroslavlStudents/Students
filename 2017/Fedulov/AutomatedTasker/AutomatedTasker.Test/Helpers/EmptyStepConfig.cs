using AutomatedTasker.StepConfig;
using AutomatedTasker.Steps;

namespace AutomatedTasker.Test.Helpers
{
    public class EmptyStepConfig : IStepConfig
    {
        public IStep Step { get; set; }
        public ExecutionCondition Condition { get; set; }
        public Status ExecutionStatus { get; set; }
        public bool Processed { private set; get; }

        public EmptyStepConfig()
        {
            Processed = false;
            ExecutionStatus = Status.NotStarted;
        }

        public bool Execute()
        {
            ExecutionStatus = Status.Success;
            Step.Execute();
            Processed = true;
            return true;
        }
    }
}