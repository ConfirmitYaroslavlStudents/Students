using System.Collections.Generic;

namespace Tasker.Core
{
    public static class ExecutionCondition
    {
        public const int Always = -2;
        public const int IfPreviousIsSuccessful = -1;

        public static int IfSuccessfulBy(int index) => index;

        public static bool CanExecute(int condition, List<State> previousStates)
        {
            if (previousStates.Count == 0 || condition == Always)
            {
                return true;
            }

            if (condition == IfPreviousIsSuccessful)
            {
                return previousStates[previousStates.Count - 1] == State.Successful;
            }

            return condition < previousStates.Count 
                   && previousStates[condition] == State.Successful;
        }
    }
}