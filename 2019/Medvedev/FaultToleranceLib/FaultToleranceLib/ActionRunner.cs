using System;

namespace FaultToleranceLib
{
    public class ActionRunner
    {
        public ActionRunnerParameters Parameters { get; }

        public ActionRunner(ActionRunnerParameters param)
        {
            Parameters = param;
        }

        public void Try(Action action)
        {
            try
            {
                DoAction(action);
            }
            catch (Exception ex)
            {
                if (Parameters.Fallbacks.ContainsKey(ex.GetType()))
                    Parameters.Fallbacks[ex.GetType()].Invoke();
                else
                    throw;
            }
        }
        private void DoAction(Action action)
        {
            for (int i = 0; i < Parameters.NumberOfTries; i++)
            {
                try
                {
                    Run(action);
                }
                catch (Exception ex)
                {
                    if (Parameters.Fallbacks.ContainsKey(ex.GetType()) && i < Parameters.NumberOfTries - 1)
                        continue;
                    throw;
                }
                break;
            }
        }

        private void Run(Action action)
        {
            if (Parameters.WithTimeout)
                throw new NotImplementedException("I have to read how to work with threads");

            action();
        }
    }
}