using System.Collections.Generic;
using AutomatedTasker.Steps;

namespace AutomatedTasker.StepConfig
{
    public class ConditionalStepConfig : IStepConfig
    {
        public IStep Step { get; set; }
        public ExecutionCondition Condition { get; set; }
        public Status ExecutionStatus { get; set; }

        public List<IStepConfig> StepsIfTrue { private set; get; }
        public  List<IStepConfig> StepsIfFalse { private set; get; }
        public IStepConfig ConditionItem { private set; get; }

        public ConditionalStepConfig(IStepConfig conditionItem, List<IStepConfig> stepsIfTrue,
            List<IStepConfig> stepsIfFalse)
        {
            ConditionItem = conditionItem;
            StepsIfTrue = stepsIfTrue;
            StepsIfFalse = stepsIfFalse;

            InitializeSteps();
        }

        private void InitializeSteps()
        {
            foreach (var step in StepsIfTrue)
            {
                step.ExecutionStatus = Status.NotStarted;
            }
        }

        public bool Execute()
        {
            ExecutionStatus = Status.Started;

            try
            {
                bool successStatus = true;
                if (ConditionItem.ExecutionStatus == Status.Success)
                {
                    foreach (var item in StepsIfTrue)
                    {
                        successStatus &= item.Execute();
                    }
                }
                else
                {
                    foreach (var item in StepsIfFalse)
                    {
                        successStatus &= item.Execute();
                    }
                }

                ExecutionStatus = Status.Success;
                return successStatus;
            }
            catch
            {
                ExecutionStatus = Status.Error;
                return false;
            }

        }
    }
}