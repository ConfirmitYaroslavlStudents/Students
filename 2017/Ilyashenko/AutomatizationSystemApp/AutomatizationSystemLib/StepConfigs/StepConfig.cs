namespace AutomatizationSystemLib
{
    public class StepConfig
    {
        public IStep Step;
        public ExecutionCondition Condition;

        public StepConfig() { }

        public StepConfig(IStep step, ExecutionCondition condition)
        {
            Step = step;
            Condition = condition;
        }

        public virtual void Execute(ProcessorInfo info, int stepId)
        {
            try
            {
                Step.Execute();
                info.StepStatus[stepId] = Status.Successful;
            }
            catch
            {
                info.StepStatus[stepId] = Status.Failed;
            }
        }
    }
}
