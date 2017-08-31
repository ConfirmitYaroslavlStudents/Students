using AutomatedTasker.Steps;

namespace AutomatedTasker.StepConfig
{
    public class OrdinaryStepConfig : IStepConfig
    {
        public ExecutionCondition Condition { set; get; }
        public Status ExecutionStatus { get; set; }
        public IStep Step { set; get; }

        public OrdinaryStepConfig(IStep step, ExecutionCondition condition)
        {
            Step = step;
            Condition = condition;
            ExecutionStatus = Status.NotStarted;
        } 

        public void Execute(ExecutingInfo info, int stepId)
        {
            ExecutionStatus = Status.Started;

            try
            {
                Step.Execute();
                ExecutionStatus = Status.Success;
            }
            catch
            {
                ExecutionStatus = Status.Error;
                info.HasFailed = true;
            }
        }
    }
}