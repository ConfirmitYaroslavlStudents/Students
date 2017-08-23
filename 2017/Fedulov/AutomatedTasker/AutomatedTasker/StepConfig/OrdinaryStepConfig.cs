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

        public bool Execute()
        {
            ExecutionStatus = Status.Started;

            try
            {
                Step.Execute();
                ExecutionStatus = Status.Success;
                return true;
            }
            catch
            {
                ExecutionStatus = Status.Error;
                return false;
            }
        }
    }
}