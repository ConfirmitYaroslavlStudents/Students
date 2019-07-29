using System;

namespace FaultTolerance
{
    public class ExceptionHandler
    {
        public ExceptionHandlerParameters Parameters { get; }

        public ExceptionHandler(ExceptionHandlerParameters parameters)
        {
            Parameters = parameters;
        }

        public void Try(IRunner runner, Action action)
        {
            try
            {
                DoAction(runner, action);
            }
            catch (Exception ex)
            {
                if (Parameters.Fallbacks.ContainsKey(ex.GetType()))
                    Parameters.Fallbacks[ex.GetType()].Invoke();
                else
                    throw;
            }
        }

        private void DoAction(IRunner runner, Action action)
        {
            for (int i = 0; i < Parameters.NumberOfTries; i++)
            {
                try
                {
                    bool succeedRun = runner.Run(action);

                    if (!succeedRun)
                        throw new RunFailedException();
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
    }
}