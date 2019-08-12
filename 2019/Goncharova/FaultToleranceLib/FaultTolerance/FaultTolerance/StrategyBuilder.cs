using System;

namespace FaultTolerance
{
    public class StrategyBuilder
    {
        internal StrategyExceptions configuredExceptions;

        public StrategyBuilder(Type exception)
        {
            configuredExceptions = new StrategyExceptions();
            configuredExceptions.Add(exception);
        }

        public StrategyBuilder Handle<T>() where T : Exception
        {
            configuredExceptions.Add(typeof(T));

            return this;
        }
    }
}
