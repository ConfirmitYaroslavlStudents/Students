using AutomatedTasker.Steps;

namespace AutomatedTasker.StepConfig
{
    public interface IStepConfig
    {
        IStep Step { set; get; }
        ExecutionCondition Condition { set; get; }
        Status ExecutionStatus { set; get; }

        void Execute(ExecutingInfo info, int stepId);
    }
}
