using System;
using System.Collections.Generic;
using System.Linq;

namespace FaultTolerance
{
    internal class StrategyExceptions
    {
        private List<Func<Exception, bool>> knownExceptionPredicates;

        internal void Add(Func<Exception, bool> exceptionPredicate)
        {
            knownExceptionPredicates = knownExceptionPredicates ?? new List<Func<Exception, bool>>();

            knownExceptionPredicates.Add(exceptionPredicate);
        }

        internal bool Contains(Exception exception)
        {
            var knownException = knownExceptionPredicates.FirstOrDefault(x => x(exception));

            return knownException != null;
        }

    }
}