using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public abstract class StrategyModel
    {
        internal StrategyModel(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            ExceptionsHandled.Add(exception);
        }

        internal StrategyModel(List<Exception> exceptions)
        {
            if (exceptions == null)
            {
                throw new ArgumentNullException(nameof(exceptions));
            }
            ExceptionsHandled.AddMany(exceptions);
        }
        internal StrategyExceptions ExceptionsHandled { get; }

    }
}
