namespace AutomatizationSystemLib
{
    public static class ExecutionConditionChecker
    {
        public static bool CheckCondition(ExecutionCondition condition, ProcessorInfo info, int stepId)
        {
            if (condition.Always)
                return true;

            if (condition.IfPreviousSucceded && stepId > 0 && info.StepStatus[stepId - 1] == Status.Successful)
                return true;

            bool stepsSucceded = true;
            foreach (var index in condition.StepsSucceded)
            {
                if (index < 0 || index > info.StepStatus.Count || info.StepStatus[index] == Status.Failed || info.StepStatus[index] == Status.Waiting)
                {
                    stepsSucceded = false;
                    break;
                }
            }
            return stepsSucceded;
        }
    }
}
