using System;

namespace FaultTolerance
{
    public class StrategyBuilder
    {
        internal StrategyExceptions configuredExceptions;

        public StrategyBuilder(Func<Exception, bool> exceptionPredicate)
        {
            configuredExceptions = new StrategyExceptions();

            configuredExceptions.Add(exceptionPredicate);
        }

        public StrategyBuilder Handle<TException>() where TException : Exception
        {
            configuredExceptions.Add((ex) => ex is TException);

            return this;
        }
    }
}
