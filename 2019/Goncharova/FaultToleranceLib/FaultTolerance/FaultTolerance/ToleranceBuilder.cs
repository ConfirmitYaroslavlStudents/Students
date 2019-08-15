using System;

namespace FaultTolerance
{
    public class ToleranceBuilder
    {
        internal ToleranceExceptions configuredExceptions;

        public ToleranceBuilder(Func<Exception, bool> exceptionPredicate)
        {
            configuredExceptions = new ToleranceExceptions();

            configuredExceptions.Add(exceptionPredicate);
        }

        public ToleranceBuilder Handle<TException>() where TException : Exception
        {
            configuredExceptions.Add((ex) => ex is TException);

            return this;
        }
    }
}
